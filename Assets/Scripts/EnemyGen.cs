using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnemyGen : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the Unity Inspector
    public int numberOfEnemies = 5;
    public float minY = 0f;
    public float maxY = 7f;
    public float minX = -8f;
    public float maxX = 8f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate random x and y positions
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            // Create a position vector with the random x and y values
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Instantiate the enemy at the spawn position and add it to the list
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    void Update()
    {
        // Check if all enemies are destroyed
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null) // If an enemy is destroyed
            {
                spawnedEnemies.RemoveAt(i); // Remove it from the list
            }
        }

        if (spawnedEnemies.Count == 0)
        {
            Debug.Log("All enemies are destroyed!");
            // Optionally, trigger some event or logic here

            SceneManager.LoadScene("End");
        }
    }
}
