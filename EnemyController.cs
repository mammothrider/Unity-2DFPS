using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BodyController {
    
    public float raycastDegree = 20;
    public float visionField = 2;

    private List<RaycastHit2D> hitList;
    
    protected override void OnAwake() {
        hitList = new List<RaycastHit2D>();
    }
    
	protected override void OnFixedUpdate() {
        Search();
    }
    
    //巡逻
    //在固定点间来回移动
    void Patrol() {} 
    
    //搜索
    //寻找敌人，并准备攻击
    void Search() {
        Vector2 targetDirection;
        Vector2 curDirection = transform.up;
        Vector2 selfPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 startPos, endPos;
        RaycastHit2D hit;
        int count = 0;
        while (count < 360 / raycastDegree) {
            startPos = selfPosition + curDirection * selfRadius;
            endPos = startPos + curDirection * visionField;
            hit = Physics2D.Linecast(startPos, endPos);
            Debug.DrawLine(startPos, endPos, Color.red);
            
            if (hit)
                hitList.Add(hit);
            
            curDirection = Quaternion.Euler(0, 0, raycastDegree) * curDirection;
            count += 1;
        }
    }
    
    //战斗
    //攻击敌人，躲避敌人攻击
    void Battle() {}
    
    //撤退
    //远离战斗区域
    void Retreat() {
        
    }
}
