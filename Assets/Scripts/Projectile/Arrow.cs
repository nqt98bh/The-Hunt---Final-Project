using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    Rigidbody2D rb;
    public int damage;
 
    

    public void Fire(Vector2 dir,float force)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir.normalized * force;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController>().TakeDamage(damage);
            Destroy(gameObject);
            
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
