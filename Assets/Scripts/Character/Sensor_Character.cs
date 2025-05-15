using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Sensor_Character:MonoBehaviour
{
    protected int ColliderCount;
    protected float DisableTimer;
    [SerializeField] protected string layerMaskName;
    

    protected void OnEnable()
    {
        ColliderCount = 0;
    }
    public bool State()
    {
        if(DisableTimer > 0) return false;
        return ColliderCount > 0;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionDetect(collision);
        ColliderCount++;

        
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        ColliderCount--;
    }
    protected void Update()
    {
        DisableTimer -= Time.deltaTime;
    }
    public void Disable(float duration)
    {
        DisableTimer = duration;
    }
    protected abstract void CollisionDetect(Collider2D collision);
}
