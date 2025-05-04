using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_Character : MonoBehaviour
{
    private int ColliderCount;
    private float DisableTimer;

    private void OnEnable()
    {
        ColliderCount = 0;
    }
    public bool State()
    {
        if(DisableTimer > 0) return false;
        return ColliderCount > 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderCount++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ColliderCount--;
    }
    private void Update()
    {
        DisableTimer -= Time.deltaTime;
    }
    public void Disable(float duration)
    {
        DisableTimer = duration;
    }
}
