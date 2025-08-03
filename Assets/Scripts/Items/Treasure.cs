using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] CoinSpawner coinSpawner;
    Vector2Int coinValue = new Vector2Int(1,10);
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("isOpened");
        }
    }
    public void SpawnCoinFromTreasure()
    {
        coinSpawner.SpawnCoin(transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity, Random.Range(coinValue.x, coinValue.y));

    }
}
