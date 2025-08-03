using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Sensor : Sensor_Character
{
    GameObject OneWayPlatform;
   public GameObject CollisionDetect()
   {
        return OneWayPlatform;

   }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var layer in layerType)
        {
            if (((1 << other.gameObject.layer) & layer) != 0)
            {
                ColliderCount++;
            }
        }
        if (other.CompareTag("OneWayPlatform"))
        {
            OneWayPlatform = other.gameObject;
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
        if (other.CompareTag("OneWayPlatform"))
        {
            OneWayPlatform = null;
        }
    }
  
}
