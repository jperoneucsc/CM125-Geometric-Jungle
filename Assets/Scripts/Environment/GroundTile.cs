using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    TileSpawner tileSpawner;


    // Start is called before the first frame update
    void Start()
    {
        tileSpawner = GameObject.FindObjectOfType<TileSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        tileSpawner.SpawnTile();
        Destroy(gameObject, 12);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
