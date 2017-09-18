using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BodyController {
    
    public float raycastDegree = 2;
    public float visionField = 2;

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
    
    //巡逻
    //在固定点间来回移动
    void Patrol() {} 
    
    //搜索
    //寻找敌人，并准备攻击
    void Search() {
        Vector3 targetDirection;
        while 
    }
    
    //战斗
    //攻击敌人，躲避敌人攻击
    void Battle() {}
    
    //撤退
    //远离战斗区域
    void Retreat() {
        
    }
}
