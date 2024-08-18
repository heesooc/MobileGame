using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BreadOven : MonoBehaviour
{
    public GameObject breadPrefab;
    public Basket basket;
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPool.Instance; 
        StartCoroutine(SpawnBreadRoutine());
    }

    private IEnumerator SpawnBreadRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (basket.GetBreadCount() < basket.maxAmount) 
            {
                GameObject bread = objectPool.GetObject(breadPrefab);
                bread.transform.position = transform.position;
                bread.SetActive(true);
                bread.GetComponent<Rigidbody>().velocity = transform.forward * 2f; 

                basket.AddBread(bread);
            }
        }
    }
}
