using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesBox : MonoBehaviour
{
    public Transform breadContainer; 
    public Vector3 breadOffset = new Vector3(0.5f, 0, 0); 
    private List<GameObject> placedBreads = new List<GameObject>();

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

                Vector3 position = breadContainer.position + breadOffset * placedBreads.Count;
                Quaternion rotation = Quaternion.Euler(0, -50, 0); 

                bread.transform.position = position; 
                bread.transform.rotation = rotation;
                placedBreads.Add(bread);
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
