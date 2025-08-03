using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceToNextMap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndedMap();
        }
    }
    public void EndedMap()
    {
        GameManager.Instance.GameFinish();
    }
}
