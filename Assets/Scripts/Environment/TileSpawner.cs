using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] section;
    private int secNum;

    public GameObject groundTile;
    Vector3 nextSpawnPoint;


    public void SpawnTile() 
    {
        secNum = Random.Range(0, 3);
        GameObject temp = Instantiate(section[secNum], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnTile();
        }
    }
}
