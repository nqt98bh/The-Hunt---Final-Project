using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : Sensor_Character
{
   protected override void CollisionDetect(Collider2D other)
   {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            Disable(0.1f);
        }
    }
}
