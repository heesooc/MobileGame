using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionAbility : MonoBehaviour
{
    public Transform handPosition; 
    private List<GameObject> holdBreads = new List<GameObject>();
    private Animator _animator;

    private Vector3 initialHandPosition;
    private Quaternion initialHandRotation;
    private bool hasInitializedHandPosition = false;

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
                StartCoroutine(StackBreadCoroutine(basket));
            }
        }
    }

    private IEnumerator StackBreadCoroutine(Basket basket)
    {
        _animator.SetBool("IsHold", true);

        yield return new WaitForSeconds(0.5f);
        _animator.speed = 0f;

        if (!hasInitializedHandPosition)
        {
            initialHandPosition = handPosition.position;
            initialHandRotation = handPosition.rotation;
            hasInitializedHandPosition = true;
        }

        while (true)
        {
            GameObject bread = basket.RemoveBread();
            if (bread != null)
            {
                holdBreads.Add(bread);
                
                Vector3 startPosition = bread.transform.position;
                Quaternion startRotation = bread.transform.rotation;

                Vector3 targetPosition = initialHandPosition + new Vector3(0, holdBreads.Count * 0.4f, 0);
                Quaternion targetRotation = initialHandRotation * Quaternion.Euler(0, 180, 0);

                float timer = 0f;
                float lerpDuration = 0.15f;

                while (timer < lerpDuration)
                {
                    bread.transform.position = Vector3.Lerp(startPosition, targetPosition, timer / lerpDuration);
                    bread.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timer / lerpDuration);
                    timer += Time.deltaTime;
                    yield return null;
                }

                bread.transform.position = targetPosition;
                bread.transform.rotation = targetRotation;
                bread.transform.SetParent(handPosition, true);

                yield return new WaitForSeconds(0.001f); 
            }
            else
            {
                break; 
            }
        }
        _animator.speed = 1f;
        CheckIfAllBreadsPlaced();
    }

    public bool HasBread()
    {
        return holdBreads.Count > 0;
    }

    public GameObject RemoveBread()
    {
        if (holdBreads.Count > 0)
        {
            GameObject bread = holdBreads[holdBreads.Count - 1];
            holdBreads.RemoveAt(holdBreads.Count - 1);

            if (holdBreads.Count == 0)
            {
                _animator.SetBool("IsHold", false);
            }
            return bread;
        }
        
        return null;
    }

    private void CheckIfAllBreadsPlaced()
    {
        if (holdBreads.Count == 0)
        {
            _animator.SetBool("IsHold", false);
        }
    }
}
