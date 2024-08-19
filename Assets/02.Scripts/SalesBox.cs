using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesBox : MonoBehaviour
{
    public Transform breadContainer;
    public Vector3 startPosition = new Vector3(-5.4f, 2f, 6f);
    public Vector3 widthOffset = new Vector3(0.7f, 0, 0);
    public Vector3 heightOffset = new Vector3(0, 0, -0.5f);
    public int itemsPerWidth = 2;

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
        int index = 0;

        while (player.HasBread())
        {
            GameObject bread = player.RemoveBread();
            if (bread != null)
            {
                bread.transform.SetParent(null);

                int width = index / itemsPerWidth;
                int height = index % itemsPerWidth;

                Vector3 position = startPosition + (height * widthOffset) + (width * heightOffset);
                Quaternion rotation = Quaternion.Euler(0, -40, 0); 

                bread.transform.position = position; 
                bread.transform.rotation = rotation;
                placedBreads.Add(bread);
                index++;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
