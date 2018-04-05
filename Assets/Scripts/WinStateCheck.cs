﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateCheck : MonoBehaviour {

    bool checkForLastEnemy;
    List<GameObject> enemyList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        EventCore.Instance.enemySpawned.AddListener(OnEnemySpawned);
        EventCore.Instance.enemyDied.AddListener(OnEnemyDied);
    }
	
    //TODO  move the event functionality to a different, single GO
    void OnEnemyDied(EventCore.EnemyDiedData data)
    {
        enemyList.Remove(data.deadEnemy);
        if (checkForLastEnemy)
        {
            if (enemyList.Count == 0)
            {
                var winData = new EventCore.WinData();
                EventCore.Instance.levelWon.Invoke(winData);
            }
        }
    }

    void OnEnemySpawned(EventCore.EnemySpawnedData data)
    {
        enemyList.Add(data.enemySpawned);
        if (data.isLastEnemy)
        {
            checkForLastEnemy = true;
        }
    }
}