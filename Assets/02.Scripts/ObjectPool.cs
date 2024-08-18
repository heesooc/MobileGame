using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    public GameObject breadPrefab;
    public GameObject moneyPrefab;
    public int poolSize = 24;

    private Queue<GameObject> breadPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool(breadPrefab, breadPool);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool(GameObject prefab, Queue<GameObject> pool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (breadPool.Count > 0)
        {
            return breadPool.Dequeue();
        }
        else
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        breadPool.Enqueue(obj);
    }
}
