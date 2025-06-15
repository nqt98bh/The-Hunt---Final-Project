using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInstanciate : MonoBehaviour
{
    [SerializeField] GameObject BossPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossPrefab.gameObject.GetComponent<Animator>().enabled = true;
            Destroy(gameObject);
        }
    }
}
