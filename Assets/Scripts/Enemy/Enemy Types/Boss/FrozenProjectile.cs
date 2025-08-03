using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FrozenProjectile : MonoBehaviour
{
    private float speed ;
    private float direction;
    private Transform target;
    private BossAI bossAI;
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
    public void Initialize(float speed, float direction, Transform target, BossAI boss)
    {
        
        this.speed = speed;
        this.direction = direction;
        this.target = target;
        this.bossAI = boss;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            direction = 0;

            CharacterController player = collision.GetComponent<CharacterController>();
            if (player.GetComponent<CharacterMovement>().isBlocking == true)
            {
                animator.SetTrigger("isBlocked");
                Destroy(gameObject, 0.5f);
            }
            else
            {
                this.direction = 0;
                player.EnterFrozen();
                Destroy(gameObject);
            }

        }
        else if (collision.CompareTag("Ground"))
        {
            animator.SetTrigger("isBlocked");
            Destroy(gameObject,0.5f);
        }
    }
}
