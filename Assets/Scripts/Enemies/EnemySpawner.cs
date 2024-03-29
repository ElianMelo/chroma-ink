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
    public EnemySpawnerData enemySpawnerData;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private TextMeshProUGUI waveText;
    private bool endLevel = false;
    private bool hadTrash = false;

    private int currentWave = 1;
    private int enemiesKilled = 0;
    private int waveEnemyQty;
    private int waveEnemySpawnQtyMax;

    private void Start()
    {
        waveEnemyQty = enemySpawnerData.waveEnemyQty;
        waveEnemySpawnQtyMax = enemySpawnerData.waveEnemySpawnQtyMax;

        waveText = GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = $"Wave {currentWave} - {enemiesKilled} / {waveEnemyQty}";
    }

    public void DisableWaveText()
    {
        waveText.gameObject.SetActive(false);
    }

    public void EnableWaveText()
    {
        waveText.gameObject.SetActive(true);
    }

    public void StartSpawner() {
        InvokeRepeating("InvokeEnemy", 0f, enemySpawnerData.spawnRate);
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

        if (enemiesKilled >= waveEnemyQty)
        {
            if (currentWave + 1 > enemySpawnerData.waveQty)
            {
                waveText.text = "Wave Cleared";
                endLevel = true;
                LevelManager.Instance.EndLevelCards();
                return;
            }
            enemiesKilled = 0;
            currentWave += 1;

            waveEnemyQty += enemySpawnerData.incrementWaveEnemyMax;
            waveEnemySpawnQtyMax += enemySpawnerData.incrementWaveSpawnSize;
        }

        waveText.text = $"Wave {currentWave} - {enemiesKilled} / {waveEnemyQty}";
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


        if (spawnedObjects.Count >= waveEnemySpawnQtyMax) { return; }
        if (enemiesKilled + spawnedObjects.Count >= waveEnemyQty) { return; }

        var enemyIndex = UnityEngine.Random.Range(0, enemies.Count);
        var spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        var spawned = Instantiate(enemies[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        spawnedObjects.Add(spawned.gameObject);

    }
}
