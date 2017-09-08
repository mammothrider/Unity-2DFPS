using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float attackDamage = 1;
    public float attackRange = 2;
    
    public float startPrecision = 0;
    public float endPrecision = 0.1f;
    
    public float flyingTime = 0.1f;
    
    public Material fireMaterial;
}
