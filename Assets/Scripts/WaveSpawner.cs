using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public int[] waveSizes;
    public float[] spawnCooldowns;
    int waveSize;
    float spawnCooldown;
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
        if (FindObjectOfType<FreezeTime>() != null)
            freezeTime = FindObjectOfType<FreezeTime>().freezeTime;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        waveSize = waveSizes[GameManager.instance.currentLevel - 1];
        spawnCooldown = spawnCooldowns[GameManager.instance.currentLevel - 1];
    }

    void Spawn()
    {
        if (waveSize > 0)
        {
            foreach (GameObject spawn in spawnPoints)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, spawn.transform.position, Quaternion.identity, null);
                var data = new EventCore.EnemySpawnedData();
                data.enemySpawned = spawnedEnemy;
                if (waveSize == 1)
                {
                    data.isLastEnemy = true;
                }
                else
                {
                    data.isLastEnemy = false;
                }
                EventCore.Instance.enemySpawned.Invoke(data);
            }
            waveSize--;
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
