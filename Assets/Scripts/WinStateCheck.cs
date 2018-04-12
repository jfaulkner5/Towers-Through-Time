using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStateCheck : MonoBehaviour
{

    bool checkForLastEnemy;
    List<GameObject> enemyList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        EventCore.Instance.enemySpawned.AddListener(OnEnemySpawned);
        EventCore.Instance.enemyDied.AddListener(OnEnemyDied);

        EventCore.Instance.levelLost.AddListener(OnLossInvoke);
    }

    //TODO  move the event functionality to a different, single GO
    void OnEnemyDied(EventCore.EnemyDiedData data)
    {
        
        enemyList.Remove(data.deadEnemy);
        if (checkForLastEnemy)
        {
            print("CHECK FOR LAST ENEMY");
            if (enemyList.Count == 0)
            {
                print("ZERO ENEMIES IN LEVEL");
                Time.timeScale = 1;

                
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                //var winData = new EventCore.WinData();
                EventCore.Instance.levelWon.Invoke();
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

    void OnLossInvoke()
    {
        // Lose function 
        print("LOSE");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        EventCore.Instance.levelLost.Invoke();

    }
}
