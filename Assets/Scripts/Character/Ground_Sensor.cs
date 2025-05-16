using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Ground_Sensor : Sensor_Character
{
   protected override void CollisionDetect(Collider2D other)
   {
        string hitLayer = LayerMask.LayerToName(other.gameObject.layer);
        Debug.Log($"[{layerType}] detected collision with layer “{hitLayer}”");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layerType) != 0)
        {
            ColliderCount++;
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layerType) != 0)
        {
            ColliderCount--;
        }
    }
}
