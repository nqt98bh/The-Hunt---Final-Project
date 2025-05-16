using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Sensor_Character:MonoBehaviour
{
    
    protected int ColliderCount =0;
    protected float DisableTimer;

    [SerializeField] protected LayerMask layerType;

    protected void OnEnable()
    {
        ColliderCount = 0;
        
    }
    public bool State()
    {
        if(DisableTimer > 0) return false;
        return ColliderCount > 0;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //ColliderCount++;
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
       // ColliderCount--;
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
