using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    Action RecycleAction;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up*5f,ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(" Collect Coin");
            RecycleAction?.Invoke();


        }
    }

    public void ReturnCoin(Action _recycle)
    {
        RecycleAction = _recycle;
        
    }
}
