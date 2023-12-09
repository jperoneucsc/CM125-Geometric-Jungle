using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] section;
    public ItemSpawner itemSpawner;
    private int secNum;

    public GameObject groundTile;
    Vector3 nextSpawnPoint;


    public void SpawnTile()
    {
        float horizontalOffset = Random.Range(-5.0f, 5.0f); // Increased range for x-axis
        secNum = Random.Range(0, 3);
        GameObject temp = Instantiate(section[secNum], nextSpawnPoint, Quaternion.identity);

        float randomXOffset = Random.Range(-5.0f, 5.0f); // Increased range for additional x-axis randomness
        bool spawnItem = Random.Range(0f, 1f) > 0.5f;
        if (spawnItem)
        {
            itemSpawner.SpawnItem(temp.transform.position + Vector3.up * 1.0f + Vector3.right * (horizontalOffset + randomXOffset));
        }
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            SpawnTile();
        }
    }
}

