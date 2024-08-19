using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInteractionAbility player = other.GetComponent<PlayerInteractionAbility>();
        if (player != null && player.HasBread())
        {
            StartCoroutine(PlaceAllBreads(player));
        }
    }

    private IEnumerator PlaceAllBreads(PlayerInteractionAbility player)
    {
        while (player.HasBread())
        {
            GameObject bread = player.RemoveBread();
            if (bread != null)
            {
                bread.transform.SetParent(null); 
                bread.transform.position = transform.position; 
                bread.transform.rotation = Quaternion.identity; 
                bread.SetActive(true);

                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
