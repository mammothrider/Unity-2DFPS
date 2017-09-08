using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    
    public float speed = 1;
    private Rigidbody2D selfRigidbody;
    private LineRenderer line;
    private Weapon selfWeapon;
    private const float selfRadius = 0.2f;
    
    private Queue<cTrajectory> drawQueue;
	// Update is called once per frame
    void Awake() {
        selfRigidbody = GetComponent<Rigidbody2D>();
        drawQueue = new Queue<cTrajectory>();
        GetWeapon(GameObject.Find("Gun"));
    }
    
    protected void MoveTowards(Vector3 direction) {
        selfRigidbody.MovePosition(transform.position + direction);
    }
    
    protected void AimAt(Vector3 direction) {
        Vector3 targetDirection = direction.normalized;
        // Debug.DrawRay(transform.position, targetDirection, Color.red);
        // Debug.DrawRay(transform.position, Vector3.up, Color.blue);
        // Debug.DrawRay(transform.position, Vector3.right, Color.green);
        
        float angle = Mathf.Atan(targetDirection.x/targetDirection.y) * Mathf.Rad2Deg;
        if (targetDirection.y < 0)
            angle = 180 + angle;
        if (angle < 0)
            angle +=  360;
        transform.eulerAngles = new Vector3(0, 0, -angle);
    }
    
    protected void GetWeapon(GameObject weapon) {
        selfWeapon = weapon.GetComponent<Weapon>();
    }
    
    protected void LoseWeapon() {
        selfWeapon = null;
    }
    
    protected cTrajectory Shoot() {
        if (selfWeapon == null)
            return null;
        
        cTrajectory tmpTra = new cTrajectory();
        //first shot
        if (drawQueue.Count == 0) {
            //self postion + radius
            tmpTra.startPos = transform.position + transform.up * selfRadius;
        }
        else {
            // + random start position around the gun
            tmpTra.startPos = transform.position + transform.up * selfRadius + transform.right * Random.Range(-1f, 1f) * selfWeapon.startPrecision;
        }
        
        tmpTra.endPos = tmpTra.startPos + transform.up * selfWeapon.attackRange + transform.right * Random.Range(-1f, 1f) * selfWeapon.endPrecision;
        
        tmpTra.time = selfWeapon.flyingTime;
        
        Debug.DrawLine(tmpTra.startPos, tmpTra.endPos, selfWeapon.fireMaterial.color);
        drawQueue.Enqueue(tmpTra);
        return tmpTra;
    }
    
    public void OnRenderObject()
    {
        if (drawQueue.Count < 0)
            return;
        // Apply the line material
        selfWeapon.fireMaterial.SetPass(0);
        Color fireColor = selfWeapon.fireMaterial.color;

        // GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        // GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        
        foreach (cTrajectory t in drawQueue)
        {
            //alpha get lower when time pass by
            GL.Color(new Color(fireColor.r, fireColor.g, fireColor.b, fireColor.a * t.time / selfWeapon.flyingTime));
            GL.Vertex(t.startPos);
            GL.Vertex(t.endPos);
            // GL.Vertex(new Vector3(0, 0, 0));
            // GL.Vertex(new Vector3(0, 1, 0));
            
            //decrease trajectory last time
            t.time -= Time.deltaTime;
        }
        
        //pop every trajectory that time is less than 0
        while (drawQueue.Count > 0 && drawQueue.Peek().time < 0) {
                drawQueue.Dequeue();
        }
        
        GL.End();
        // GL.PopMatrix();
    }
}
