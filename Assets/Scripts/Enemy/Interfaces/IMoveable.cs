using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Move(Rigidbody2D rb, float dir, float speed);
}
