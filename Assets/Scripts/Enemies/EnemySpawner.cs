using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<Transform> spawnPoints;
    public float startDelay = 1f;
    public float spawnRate = 1f;
    public int waveQuantity = 3;
    public int maxBaseWaveEnemies = 10;
    public int waveIncrement = 5;
    public int maxEnemiesWave = 3;
    public int maxnemiesWaveIncrement = 1;

    private int currentWave = 1;
    private int baseWaveEnemies = 0;
    private int enemiesKilled = 0;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private TextMeshProUGUI waveText;
    private bool endLevel = false;
    private bool hadTrash = false;

    private void Start()
    {
        baseWaveEnemies = maxBaseWaveEnemies;

        waveText = GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = $"Wave {currentWave} - {enemiesKilled} / {maxBaseWaveEnemies}";

        InvokeRepeating("InvokeEnemy", 2f, AttributeManager.Instance.enemiesSpawnRate);
    }

    private void OnEnable()
    {
        Enemy.onDeathEvent += EnemyDeathEvent;
    }

    private void OnDisable()
    {
        Enemy.onDeathEvent -= EnemyDeathEvent;
    }

    private void EnemyDeathEvent()
    {
        UpdateWave();
    }

    private void UpdateWave()
    {
        if (endLevel) { return; }
        
        enemiesKilled += 1;
        baseWaveEnemies -= 1;

        if (enemiesKilled >= maxBaseWaveEnemies)
        {
            if (currentWave + 1 > waveQuantity)
            {
                waveText.text = "";
                endLevel = true;
                LevelManager.Instance.EndLevelCards();
            }
            enemiesKilled = 0;
            currentWave += 1;
            maxBaseWaveEnemies += waveIncrement;
            baseWaveEnemies = maxBaseWaveEnemies;
            maxEnemiesWave += maxnemiesWaveIncrement;
        }

        waveText.text = $"Wave {currentWave} - {enemiesKilled} / {maxBaseWaveEnemies}";
    }

    private void InvokeEnemy()
    {
        if (endLevel) { return; }

        do
        {
            hadTrash = false;
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (spawnedObjects[i] == null)
                {
                    spawnedObjects.Remove(spawnedObjects[i]);
                    hadTrash = true;
                }
            }
        } while (hadTrash);

        if (spawnedObjects.Count <= baseWaveEnemies && spawnedObjects.Count < maxEnemiesWave)
        {
            var enemyIndex = UnityEngine.Random.Range(0, enemies.Count);
            var spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            var spawned = Instantiate(enemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.Euler(0f,0f,0f));
            spawnedObjects.Add(spawned.gameObject);
        }
    }
}
