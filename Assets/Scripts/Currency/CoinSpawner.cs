using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] PoolManager pool;

    public void SpawnCoin(Vector3 position, Quaternion rotation,int coinValue)
    {
        GameObject coinGO = pool.GetObject(position, rotation);
        var coin = coinGO.GetComponent<Coin>();
        coin.ReturnCoin(() => { 
            pool.ReturnToPool(coinGO);
            
        });
    }
   
}
