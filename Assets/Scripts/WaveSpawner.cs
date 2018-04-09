using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public int waveSize;
    public float spawnCooldown;
    int currentEnemiesLeft;
    float currentSpawnTimer;
    float freezeTime;
    public GameObject enemyPrefab;
    GameObject[] spawnPoints;

    public static WaveSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("MULTIPLE GAME MANAGERS IN SCENE");
            DestroyImmediate(this);
        }
        EventCore.Instance.eventFreeze.AddListener(Freeze);
    }

    void Freeze(EventCore.FreezeData data)
    {
        currentSpawnTimer += freezeTime;
    }

    public void Initialize()
    {
        freezeTime = FindObjectOfType<FreezeTime>().freezeTime;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
    }

    void Spawn()
    {
        foreach (GameObject spawn in spawnPoints)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawn.transform.position, Quaternion.identity, null);
            var data = new EventCore.EnemySpawnedData();
            data.enemySpawned = spawnedEnemy;
            if (currentEnemiesLeft == 1)
            {
                data.isLastEnemy = true;
            }
            else
            {
                data.isLastEnemy = false;
            }
            EventCore.Instance.enemySpawned.Invoke(data);
        }
    }

    private void Update()
    {
        currentSpawnTimer -= Time.deltaTime;
        if (currentSpawnTimer <= 0)
        {
            Spawn();
            currentSpawnTimer = spawnCooldown;
        }
    }
}
