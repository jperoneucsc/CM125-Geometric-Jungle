using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<TempMovement>().CollectableCollected();
            // Destroy the item when the player collides with it
            Destroy(gameObject);
        }
    }
}
