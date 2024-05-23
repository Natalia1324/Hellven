using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public TerrainGenerator mapInfo;
    public GameObject enemyPrefab; // Assign the enemy prefab in the Inspector
    public Transform[] spawnPoints; // Assign spawn points in the Inspector
    public float spawnInterval = 3f;

    private void Start()
    {
        Reposition();
        spawnPoints = GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
            Reposition();
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }

    private void Reposition()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float x = mapInfo.radius * Mathf.Cos(angle);
        float y = mapInfo.radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, 0);
    }   
}


