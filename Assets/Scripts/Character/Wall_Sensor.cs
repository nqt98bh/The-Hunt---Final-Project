using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Sensor : Sensor_Character
{
  

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        foreach(var layer in layerType)
        {
            if (((1 << other.gameObject.layer) & layer) != 0)
            {
                ColliderCount++;
            }
        }
        
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        foreach (var layer in layerType)
        {
            if (((1 << other.gameObject.layer) & layer) != 0)
            {
                ColliderCount--;
            }
        }
    }
}
