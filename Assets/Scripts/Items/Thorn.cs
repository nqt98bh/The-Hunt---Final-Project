using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Thorn : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float timeInterval = 1f;
     private Coroutine coroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coroutine = StartCoroutine( DamageOverTime(timeInterval));
            Debug.Log("Step on Thorn!!!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    IEnumerator DamageOverTime(float time)
    {
        while (!GameManager.Instance.isGameOver)
        {
            ApplyDamage((int) damage);
            yield return new WaitForSeconds(time);

        }
    }
    private void ApplyDamage(int damage)
    {
        GameManager.Instance.CharacterController.TakeDamage(damage);
    }
}
