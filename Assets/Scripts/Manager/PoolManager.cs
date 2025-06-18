using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize;
    private Queue<GameObject> poolQueue = new Queue<GameObject>();
    private void Awake()
    {
        PoolSetUp();
    }
    private void PoolSetUp()
    {
        for(int i =0; i< poolSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            poolQueue.Enqueue(go);
        }
    }

    public GameObject GetObject(Vector3 position,Quaternion rotation)
    {
        if(poolQueue.Count == 0)
        {
            GameObject newGo = Instantiate(prefab);
            newGo.transform.position = position;
            newGo.transform.rotation = rotation;
            return newGo;
        }
        GameObject go = poolQueue.Dequeue();
        go.transform.position = position;
        go.transform.rotation = rotation;
        Init(go);
        return go;

    }
    public void Init(GameObject go)
    {
        go.SetActive(true);
    }
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        poolQueue.Enqueue(go);
    }
    public Queue<GameObject> GetPoolQueue()
    {
        return poolQueue;
    }
}
