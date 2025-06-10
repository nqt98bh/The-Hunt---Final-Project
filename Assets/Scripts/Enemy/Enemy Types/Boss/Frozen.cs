using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    private float speed ;
    private float direction;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        PrjMovement();
    }
    void PrjMovement()
    {
        if (direction == 0) { return; }
        transform.position += new Vector3(direction,0,0) * speed * Time.deltaTime;

    }
    public void Initialize(float speed, float direction, Transform target)
    {
        
        this.speed = speed;
        this.direction = direction;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            direction = 0;
            if (collision.GetComponent<CharacterMovement>().isBlocking == true)
            {
                animator.SetTrigger("isBlocked");
                Destroy(gameObject, 0.5f);
            }
            else
            {
                this.direction = 0;
                animator.SetTrigger("isFrezingPlayer");
            }

        }
    }
}
