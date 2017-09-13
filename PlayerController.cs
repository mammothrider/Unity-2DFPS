using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BodyController {
	void FixedUpdate () {
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
        
        if (Input.GetMouseButton(0))
            Shoot();
	}
}
