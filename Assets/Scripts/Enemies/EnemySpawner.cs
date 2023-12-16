using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<Transform> spawnPoints;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private void Start()
    {
        InvokeRepeating("InvokeEnemy", 1f, AttributeManager.Instance.enemiesSpawnRate);
        // StartCoroutine(InvokeEnemy());
    }

    private void InvokeEnemy()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if(spawnedObjects[i] == null)
            {
                spawnedObjects.Remove(spawnedObjects[i]);
            }
        }
        if(spawnedObjects.Count < AttributeManager.Instance.maxEnemies)
        {
            var enemyIndex = Random.Range(0, enemies.Count);
            var spawnIndex = Random.Range(0, spawnPoints.Count);
            var spawned = Instantiate(enemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
            spawnedObjects.Add(spawned.gameObject);
        }
    }
}
