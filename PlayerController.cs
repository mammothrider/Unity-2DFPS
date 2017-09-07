using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BodyController {

    public Material lineMat;

	void Update () {
		float ver = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        Vector3 direction = (Vector3.up * ver + Vector3.right * hor) * speed * Time.deltaTime;
        // transform.position += (transform.up * ver + transform.right * hor) * speed * Time.deltaTime;
        // transform.position += (Vector3.up * ver + Vector3.right * hor) * speed * Time.deltaTime;
        MoveTowards(direction);
        // Debug.Log(direction);
        
        Vector3 targetDirection = mousePos - transform.position;
        AimAt(targetDirection);
        
	}
    void FixedUpdate() {
         GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(2, 2, -2);
        GL.End();
    }
    
    void OnDrawGizmos() {
        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(2, 2, -2);
        GL.End();
    }
}
