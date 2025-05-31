using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : IMoveable
{
    public void Move(Rigidbody2D rb, float dir, float speed)
    {
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

  
}
