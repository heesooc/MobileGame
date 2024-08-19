using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour, IBreadProvider
{
    public int maxAmount = 8;
    private List<GameObject> breads = new List<GameObject>();

    public float offsetRange = 0.5f;

    private void Start()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            GameObject bread = ObjectPool.Instance.GetObject(null);
            Vector3 position = transform.position;
            position.y = 1;
            position.x += Random.Range(-offsetRange, offsetRange);
            position.z += Random.Range(-offsetRange, offsetRange);

            bread.transform.position = position;
            bread.SetActive(true);
            breads.Add(bread);
        }
    }

    public int GetBreadCount()
    {
        return breads.Count;
    }

    public void AddBread(GameObject bread)
    {
        if (breads.Count < maxAmount)
        {
            breads.Add(bread);
            bread.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Vector3 position = transform.position;
            position.y = 1;
            position.x += Random.Range(-offsetRange, offsetRange);
            position.z += Random.Range(-offsetRange, offsetRange);
            bread.transform.position = position;
        }
    }

    public GameObject RemoveBread()
    {
        if (breads.Count > 0)
        {
            GameObject bread = breads[0];
            breads.RemoveAt(0);
            return bread;
        }
        return null;
    }
}
