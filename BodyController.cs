using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    
    public float speed = 1;
    private Rigidbody2D selfRigidbody;
	// Update is called once per frame
    void Awake() {
        selfRigidbody = GetComponent<Rigidbody2D>();
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
}
