using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    public GameObject[] treePrefabs;
    public GameObject[] treePrefabs2;
    public GameObject[] branchPrefabs;
    public GameObject[] bushPrefabs;
    public GameObject[] stumpPrefabs;

    public Transform[] spawnPoints;

    public void GenerateRandomObstacles()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Randomly choose an array of obstacle prefabs
            GameObject[] obstacleArray = GetRandomObstacleArray();

            // Randomly choose an obstacle prefab from the selected array
            GameObject randomObstaclePrefab = obstacleArray[Random.Range(0, obstacleArray.Length)];

            // Instantiate the obstacle at the spawn point
            GameObject obstacle = Instantiate(randomObstaclePrefab, spawnPoint.position, spawnPoint.rotation, transform);

            // If the obstacle is a rock scale by:
            if (obstacleArray == rockPrefabs)
            {
                // Randomize the scale
                Vector3 randomScale = new Vector3(
                    Random.Range(1f, 1.5f),
                    Random.Range(1f, 2f),
                    Random.Range(1f, 1.5f)
                );
                obstacle.transform.localScale = randomScale;

                // Randomize the rotation
                Quaternion randomRotation = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f), // Random rotation around Y-axis
                0f
                );

                obstacle.transform.rotation = randomRotation;
            }

            // If the obstacle is a tree scale by:
            if (obstacleArray == treePrefabs || obstacleArray == treePrefabs2)
            {
                // Randomize the scale
                Vector3 randomScale = new Vector3(
                    Random.Range(2f, 2.2f),
                    Random.Range(2f, 2.3f),
                    Random.Range(2f, 2.2f)
                );
                obstacle.transform.localScale = randomScale;

                // Randomize the rotation
                Quaternion randomRotation = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f), // Random rotation around Y-axis
                0f
                );

                obstacle.transform.rotation = randomRotation;
            }

            // If the obstacle is a branch scale by:
            if (obstacleArray == branchPrefabs)
            {
                // Randomize the scale
                Vector3 randomScale = new Vector3(
                    Random.Range(2f, 3f),
                    Random.Range(4f, 5f),
                    Random.Range(4f, 5f)
                );
                obstacle.transform.localScale = randomScale;
            }

            // If the obstacle is a stump scale by:
            if (obstacleArray == stumpPrefabs)
            {
                // Randomize the scale
                Vector3 randomScale = new Vector3(
                    Random.Range(1.5f, 1.7f),
                    Random.Range(1.5f, 2f),
                    Random.Range(1.5f, 1.7f)
                );
                obstacle.transform.localScale = randomScale;

                // Randomize the rotation
                Quaternion randomRotation = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f), // Random rotation around Y-axis
                0f
                );

                obstacle.transform.rotation = randomRotation;
            }


            // If the obstacle is a bush scale by:
            if (obstacleArray == bushPrefabs)
            {
                // Randomize the scale
                Vector3 randomScale = new Vector3(
                    Random.Range(2.5f, 3f),
                    Random.Range(3f, 3.5f),
                    Random.Range(2.5f, 3f)
                );
                obstacle.transform.localScale = randomScale;

                // Randomize the rotation
                Quaternion randomRotation = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f), // Random rotation around Y-axis
                0f
                );

                obstacle.transform.rotation = randomRotation;
            }

        }
    }


    GameObject[] GetRandomObstacleArray()
    {
        // Create an array that contains all the obstacle arrays
        GameObject[][] allObstacleArrays = { rockPrefabs, treePrefabs, treePrefabs2, branchPrefabs, bushPrefabs, stumpPrefabs};

        // Randomly choose an obstacle array
        GameObject[] selectedObstacleArray = allObstacleArrays[Random.Range(0, allObstacleArrays.Length)];

        return selectedObstacleArray;
    }

    // Example usage in Start method (you can adjust when to call this based on your game logic)
    void Start()
    {
        GenerateRandomObstacles();
    }
}
