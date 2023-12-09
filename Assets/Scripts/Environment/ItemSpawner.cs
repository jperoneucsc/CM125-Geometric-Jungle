using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    private int itemNum;

    // List of spawn points
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        SpawnItems();
    }

    public void SpawnItem(Vector3 spawnPosition)
    {
        itemNum = Random.Range(0, items.Length);
        float heightOffset = (Random.Range(0, 2) == 0) ? 0.05f : 1.8f;

        // Instantiate the chosen item at the specified position
        GameObject newItem = Instantiate(items[itemNum], spawnPosition + Vector3.up * heightOffset, Quaternion.identity);
        ItemManager itemManager = newItem.AddComponent<ItemManager>();
    }

    // Function to spawn items at all specified spawn points
    void SpawnItems()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnItem(spawnPoint.position);
        }
    }


    void Update()
    {

    }
}