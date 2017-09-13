using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float attackDamage = 1;
    public float attackRange = 2;
    
    public float startPrecision = 0;
    public float endPrecision = 0.1f;
    
    public float flyingTime = 0.1f;
    
    public float coolDownTime = 1f;
    public int magazine = 6;
    public float reloadTime = 5f;
    
    public Material fireMaterial;
    public Color fireColor;
    
    public float recoil = 1f;
    
    private float _cooldown = 0;
    private float _reload = 0;
    private int _magazine = 0;
    
    private Queue<cTrajectory> drawQueue;
    
    void Awake() {
        _magazine = magazine;
        drawQueue = new Queue<cTrajectory>();
    }
    
    void FixedUpdate() {
        if (_cooldown > 0)
            _cooldown -= Time.fixedDeltaTime;
        if (_reload > 0)
            _reload -= Time.fixedDeltaTime;
        
        if (_magazine == 0)
            Reload();
    }
    
    public bool ReadyToFire() {
        if (_magazine > 0 && _cooldown <= 0 && _reload <= 0)
            return true;
        return false;
    }
    
    //transform for direction, and direction is always transform.up
    //radius for position
    public void Fire(Transform shooter, float radius) {
        if (!ReadyToFire())
            return;
        
        _magazine -= 1;
        _cooldown = coolDownTime;
        
        
        cTrajectory tmpTra = new cTrajectory();
        //first shot
        if (drawQueue.Count == 0) {
            //self postion + radius
            tmpTra.startPos = shooter.position + shooter.up * radius;
        }
        else {
            // + random start position around the gun
            tmpTra.startPos = shooter.position + shooter.up * radius + shooter.right * Random.Range(-1f, 1f) * startPrecision;
        }
        
        //no collision end position
        tmpTra.endPos = tmpTra.startPos + shooter.up * attackRange + shooter.right * Random.Range(-1f, 1f) * endPrecision;
        
        //recoil
        shooter.GetComponent<BodyController>().PushBody(recoil, tmpTra.startPos, tmpTra.startPos - tmpTra.endPos);
        
        //find collision
        RaycastHit2D hit = Physics2D.Linecast(tmpTra.startPos, tmpTra.endPos);
        if (hit) {
            tmpTra.endPos = hit.point;
            //get shot, push back
            if (hit.rigidbody != null) {
                BodyController controller = hit.rigidbody.GetComponent<BodyController>();
                if (controller) {
                    controller.GetShot(this, tmpTra.endPos, tmpTra.endPos - tmpTra.startPos);
                }
            }
        }
        
        tmpTra.time = flyingTime;
        
        drawQueue.Enqueue(tmpTra);
    }
    
    public void Reload() {
        _reload = reloadTime;
        _magazine = magazine;
    }
    
    
    //Gun fire trajectory rendering
    public void OnRenderObject()
    {
        if (drawQueue.Count < 0)
            return;
        // Apply the line material
        fireMaterial.SetPass(0);

        // GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        // GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        
        foreach (cTrajectory t in drawQueue)
        {
            //alpha get lower when time pass by
            Color tmpColor = new Color(fireColor.r, fireColor.g, fireColor.b, fireColor.a * t.time / flyingTime);
            GL.Color(tmpColor);
            GL.Vertex(t.startPos);
            GL.Vertex(t.endPos);
            
            //decrease trajectory last time
            t.time -= Time.deltaTime;
            
            Debug.DrawLine(t.startPos, t.endPos, tmpColor);
        }
        
        GL.End();
        // GL.PopMatrix();
        
        //pop every trajectory that time is less than 0
        while (drawQueue.Count > 0 && drawQueue.Peek().time < 0) {
                drawQueue.Dequeue();
        }
    }
}
