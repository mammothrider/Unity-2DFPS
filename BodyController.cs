using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    
    public float speed = 1;
    
    public float health = 100;
    public bool death = false;
    
    public Sprite deadBody;
    private Sprite normalBody;
    
    private Rigidbody2D selfRigidbody;
    private Weapon selfWeapon;
    protected const float selfRadius = 0.3f;
    
    
	// Update is called once per frame
    void Awake() {
        selfRigidbody = GetComponent<Rigidbody2D>();
        normalBody = GetComponent<SpriteRenderer>().sprite;
        
        OnAwake();
    }
    
    protected virtual void OnAwake() {}
    
    void Start() {
        Transform tmp = transform.Find("Gun");
        if (tmp)
            GetWeapon(tmp.GetComponent<Weapon>());
    }
    
    void FixedUpdate() {
        if (health < 0)
            Dead();
        
        if (!death)
            OnFixedUpdate();
    }
    
    protected virtual void OnFixedUpdate() {}
    
    protected void MoveTowards(Vector3 direction) {
        selfRigidbody.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
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
    
    protected void AimAtPosition(Vector3 position) {
        Vector3 direction = position - transform.position;
        AimAt(direction);
    }
    
    protected void GetWeapon(Weapon weapon) {
        if (weapon)
            selfWeapon = weapon;
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
    
    public void Dead() {
        if (!death) {
            Debug.Log("Dead!");
            GetComponent<SpriteRenderer>().sprite = deadBody;
            death = true;
            selfRigidbody.freezeRotation = true;
        }
    }
}
