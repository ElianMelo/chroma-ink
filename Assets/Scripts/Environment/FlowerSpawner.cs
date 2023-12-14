using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public List<GameObject> flowers;
    public List<Transform> spawnPoints;
    public int maxFlower;

    private Dictionary<Transform, GameObject> instances = new Dictionary<Transform, GameObject>();
    private bool waitingSpawn = false;

    void Start()
    {
        FillFlowers();
        InvokeRepeating("CheckPositions", 1f, 1f);
    }

    private void CheckPositions()
    {
        var hasDestroied = false;
        for (int i = 0; i < instances.Count; i++)
        {
            if (instances.ContainsKey(spawnPoints[i]) && instances[spawnPoints[i]] == null)
            {
                instances.Remove(spawnPoints[i]);
                hasDestroied = true;
            }
            if(instances.Count < maxFlower)
            {
                hasDestroied = true;
            }
        }
        if(hasDestroied && !waitingSpawn)
        {
            waitingSpawn = true;
            StartCoroutine(WaitDelayRespawn());
        }
    }

    private IEnumerator WaitDelayRespawn()
    {
        yield return new WaitForSeconds(AttributeManager.Instance.flowerRespawnRate);
        AddFlower(true);
        waitingSpawn = false;
    }

    private void FillFlowers()
    {
        while (instances.Count < maxFlower)
        {
            AddFlower();
        }
    }

    private void AddFlower(bool stress = false)
    {
        do
        {
            int valFlower = Random.Range(0, flowers.Count);
            GameObject selectedFlower = flowers[valFlower];

            int valSpawn = Random.Range(0, spawnPoints.Count);
            Transform selectedSpawn = spawnPoints[valSpawn];

            if (!instances.ContainsKey(selectedSpawn))
            {
                GameObject flower = Instantiate(selectedFlower, selectedSpawn.position, Quaternion.identity);
                instances.Add(selectedSpawn, flower);
                stress = false;
            }
        } while (stress);
    }
}
