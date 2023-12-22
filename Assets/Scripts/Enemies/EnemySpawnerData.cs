using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemySpawnerData", order = 4)]
public class EnemySpawnerData : ScriptableObject
{
    // Spawn Rate
    public float spawnRate = 1f;

    // Wave Quantity
    public int waveQty = 2;

    // Max enemy spawned per spawn
    public int waveEnemySpawnQtyMax = 3;
    // Max enemy in wave
    public int waveEnemyQty = 5;

    // Increment spawn size
    public int incrementWaveSpawnSize = 1;
    // Increment max enemy
    public int incrementWaveEnemyMax = 1;
}
