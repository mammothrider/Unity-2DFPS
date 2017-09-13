using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    
    public float speed = 1;
    
    public float health = 100;
    
    private Rigidbody2D selfRigidbody;
    private Weapon selfWeapon;
    private const float selfRadius = 0.3f;
    
    
	// Update is called once per frame
    void Awake() {
        selfRigidbody = GetComponent<Rigidbody2D>();
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
        // transform.eulerAngles = new Vector3(0, 0, -angle);
        selfRigidbody.MoveRotation(-angle);
    }
    
    protected void GetWeapon(GameObject weapon) {
        selfWeapon = weapon.GetComponent<Weapon>();
    }
    
    protected void LoseWeapon() {
        selfWeapon = null;
    }
    
    protected void Shoot() {
        if (!selfWeapon || !selfWeapon.ReadyToFire())
            return;
        
        selfWeapon.Fire(transform, selfRadius);
        // cTrajectory tmpTra = selfWeapon.Fire(transform, selfRadius);
        // PushBody(selfWeapon.recoil, tmpTra.startPos, tmpTra.startPos - tmpTra.endPos);
        
    }
    
    public void GetShot(Weapon shooter, Vector2 hitPosition, Vector2 direction) {
        health -= shooter.attackDamage;
        PushBody(shooter.recoil, hitPosition, direction);
    }
    
    public void PushBody(float force, Vector2 position, Vector2 direction) {
        // Debug.DrawRay(position, direction.normalized * force / 100, Color.yellow, 1);
        // Debug.Log(direction.normalized * force);
        selfRigidbody.AddForceAtPosition(direction.normalized * force, position);
    }
}
