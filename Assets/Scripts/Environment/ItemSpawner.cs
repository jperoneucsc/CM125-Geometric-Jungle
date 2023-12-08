using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    private int itemNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnItem(Vector3 spawnPosition)
    {
        itemNum = Random.Range(0, items.Length);
        float heightOffset = (Random.Range(0, 2) == 0) ? 0.05f : 2.3f; //Random.Range(1f, 3.5f);

        // Instantiate the chosen item at the specified position
        GameObject newItem = Instantiate(items[itemNum], spawnPosition + Vector3.up * heightOffset, Quaternion.identity);
        ItemManager itemManager = newItem.AddComponent<ItemManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
