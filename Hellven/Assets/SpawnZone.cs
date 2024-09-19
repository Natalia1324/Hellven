using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDistance = 20f;
    public float despawnDistance = 40f;
    public Transform player;
    public int baseEnemyLevel = 1;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private BoxCollider2D boxCollider; // Reference to the BoxCollider2D

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > spawnDistance && distanceToPlayer < despawnDistance && spawnedEnemies.Count == 0)
        {
            SpawnEnemies();
        }
        else if (distanceToPlayer > despawnDistance && spawnedEnemies.Count > 0)
        {
            DespawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        int totalKills = EnemyManager.Instance.GetGlobalKillCount();
        int numberOfEnemies = Mathf.Max(1, Mathf.CeilToInt(Mathf.Sqrt(totalKills + 10) * UnityEngine.Random.Range(1f, 2f)));

        Vector2 center = transform.position;
        Vector2 size = boxCollider.size; // Get the size of the BoxCollider2D

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Randomly spread enemies within the box collider bounds
            float randomX = UnityEngine.Random.Range(-size.x / 2, size.x / 2);
            float randomY = UnityEngine.Random.Range(-size.y / 2, size.y / 2);
            Vector2 spawnPosition = new Vector2(center.x + randomX, center.y + randomY);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            int enemyLevel = GetEnemyLevel();
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.SetLevel(enemyLevel);
            }

            spawnedEnemies.Add(enemy);
        }
    }

    void DespawnEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy == null) continue;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.despawn();
            }
        }
        spawnedEnemies.Clear();
    }

    int GetEnemyLevel()
    {
        return baseEnemyLevel + EnemyManager.Instance.GetGlobalEnemyLevel();
    }
}
