using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentLevel;
    public float totalLevels;


    public Dictionary<float, bool> levelsBeat = new Dictionary<float, bool>();



    public static GameManager instance;

    //[Jay's edits]
    #region
    private GameObject[] objs; //temp array for obj grabbing
    public List<GameObject> enemyList;

    public GameObject closestEnemy;

    #endregion

    private void Awake()
    {
        if  (instance != null)
        {
            DestroyImmediate(this);
            Debug.LogError("MULTIPLE GAMEMANAGERS IN SCENE");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int index = 1; index < totalLevels + 1; index++)
        {
            instance.levelsBeat.Add(index, false);
        }
    }

    // Jay's function additons
    #region

        /// <summary>
        /// This section is the basic function for the towers to call upon and finds the closest enemy to the tower.
        /// Since only one tower should be calling it at a time, it shouldn't be an issue of performance.
        /// towerCalling is passing the tower  that called the function through.
        /// </summary>

    public GameObject EnemyToAttack(GameObject towerCalling)
    {
        //call function the create list of current enemies
        EnemyList();

        foreach(GameObject enemy in enemyList)
        {
            if(closestEnemy == null)
            {
                closestEnemy = this.gameObject;
            }
            else if(Vector3.Distance(towerCalling.transform.position, enemy.transform.position) <= Vector3.Distance(closestEnemy.transform.position, towerCalling.transform.position))
            {
                closestEnemy = enemy; 
            }
        }
        return closestEnemy;
    } 

    void EnemyList()
    {
        objs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in objs)
        {
            enemyList.Add(Enemy);
        }
    }
    #endregion

}
