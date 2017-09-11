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
    
    private float _cooldown = 0;
    private float _reload = 0;
    private int _magazine = 0;
    
    void Start() {
        _magazine = magazine;
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
    
    public void Fire() {
        if (ReadyToFire()) {
            _magazine -= 1;
            _cooldown = coolDownTime;
        }
    }
    
    public void Reload() {
        _reload = reloadTime;
        _magazine = magazine;
    }
    
}
