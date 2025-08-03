using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] PoolManager pool;

    public void SpawnCoin(Vector3 position, Quaternion rotation, int coinValue)
    {
        //GameObject arrowGO = pool.GetObject(position, rotation);
        //var arrow = arrowGO.GetComponent<Arrow>();
        //arrow.ReturnArrow(() => {
        //    pool.ReturnToPool(arrowGO);
        //    GameManager.Instance.PlaySoundFX(SoundType.playerCollect);
        //    GameManager.Instance.CurencyManager.AddCoin(coinValue);
        //});
    }
}
