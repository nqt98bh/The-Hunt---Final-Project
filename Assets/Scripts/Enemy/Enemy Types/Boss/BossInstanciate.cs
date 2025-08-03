using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInstanciate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.GetComponentInParent<Animator>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(ActiveBoss(5f));
            Debug.Log("Enable Animation");
        }
    }

    IEnumerator ActiveBoss(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponentInParent<BossAI>().enabled = true;
        Debug.Log("Enable BossAI");
        Destroy(gameObject);
        Debug.Log("Destroy");

    }
}
