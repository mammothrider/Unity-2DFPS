using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour {
    
    public Transform target;
    
    // public Material lineMat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
    // void OnPostRender() {
        // GL.Begin(GL.LINES);
        // lineMat.SetPass(0);
        // GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
        // GL.Vertex3(0, 0, 0);
        // GL.Vertex3(2, 2, -2);
        // GL.End();
    // }
}
