using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    Rigidbody2D rb;
    Collider2D collider2D;
    public int damage;

    public Vector2 attackPoint;
    public Vector2 midPoint;
    public Vector2 targetPoint;
    public float timeDuration;
    float elapsed=0;
    [SerializeField] LayerMask GroundLayer;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();    
    }
    private void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed/timeDuration);
        float u = 1 - t;

        Vector3 bezierPos = (u*u)*attackPoint + (2f*u*t)*midPoint + (t*t)*targetPoint;
        transform.position = bezierPos;

        Vector3 velocity = 2f * u * (midPoint - attackPoint) + 2f * t * (targetPoint - midPoint);
        if(velocity.sqrMagnitude > Mathf.Epsilon)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    public void Initialize(Vector3 start, Vector3 mid, Vector3 end, float travelTime, int damage)
    {
        attackPoint = start;
        midPoint = mid;
        targetPoint = end;
        timeDuration = travelTime;
        this.damage = damage;
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
            collider2D.enabled = false;
            Destroy(gameObject,2f);
        }
    }
}
