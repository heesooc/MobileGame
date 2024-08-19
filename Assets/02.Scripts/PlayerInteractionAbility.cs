using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionAbility : MonoBehaviour
{
    public Transform handPosition; 
    private List<GameObject> holdBreads = new List<GameObject>();
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            Basket basket = other.GetComponent<Basket>();
            if (basket != null)
            {
                GameObject bread = basket.RemoveBread();
                if (bread != null)
                {
                    bread.transform.SetParent(handPosition);

                    Vector3 localPosition = new Vector3(-holdBreads.Count * 0.4f, 0, 0);
                    bread.transform.localPosition = localPosition;
                    bread.transform.localRotation = Quaternion.Euler(0, 180, -90);
                    holdBreads.Add(bread);
                    _animator.SetBool("IsHold", true);
                }
            }
        }
    }
}
