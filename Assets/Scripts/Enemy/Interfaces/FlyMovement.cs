using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : IMoveable
{
    public void Move(Rigidbody2D rb, float dir, float speed)
    {
        float hover = Mathf.Sin(Time.time * 2f) * 0.5f;
        rb.velocity = new Vector2(dir*speed, hover);
    }

  
}
