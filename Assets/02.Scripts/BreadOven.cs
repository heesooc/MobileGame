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
                bread.transform.position = new Vector3(-5f, 1.9f, -4.56f);
                Rigidbody rb = bread.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                bread.SetActive(true);
                
                yield return new WaitForSeconds(1f);
                
                rb.isKinematic = false;
                rb.velocity = transform.forward * 2f;
                
                yield return new WaitForSeconds(0.5f);
                rb.isKinematic = true;

                basket.AddBread(bread);
            }
        }
    }
}
