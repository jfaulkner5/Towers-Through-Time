using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    public static EventCore Instance;


    //ADD NEW EVENTS HERE PLEASE
    #region
    //data and event for when an enemy is spawned
    public class EnemySpawnedData
    {
        public bool isLastEnemy;
        public GameObject enemySpawned;
    }

    public class UnityEventEnemySpawned : UnityEvent<EnemySpawnedData> { }
    public UnityEventEnemySpawned enemySpawned = new UnityEventEnemySpawned();

    public class WinData
    {
        public int currentLevel;
    }

    //public class UnityEventLevelWon : UnityEvent<WinData> { }
    //public UnityEventLevelWon levelWon = new UnityEventLevelWon();
    public UnityEvent levelWon;

    public UnityEvent levelLost;

    public class EnemyDiedData
    {
        public GameObject deadEnemy;
    }
    public class UnityEventEnemyDied : UnityEvent<EnemyDiedData> { }
    public UnityEventEnemyDied enemyDied = new UnityEventEnemyDied();
    #endregion

    ////Game Over
    //public delegate void GameCore();

    //public static event GameCore PyramidAttack, PyramidDamage, PyramidRepair;

    public UnityEvent testEvent;
    public UnityEvent externalEventTest;


    //Audio events
    #region

    public UnityEvent playerWalk;

    public UnityEvent towerOn, towerOff, towerFire, towerFireStop;

    public UnityEvent pyramidHit, pyramidDestroy, pyramidHeal;

    public UnityEvent enemySpawnAudio, enemyDeathAudio, enemyWalkAudio, enemyAttackAudio;

    #endregion

    //Hack lol
    //Enemy Spawner stuff
    #region

    public UnityEvent enemySpawn, enemyDeath;

    #endregion

    
    #region Tower stuff

    public class UnityEventGameObject : UnityEvent<GameObject> { }
    public UnityEventGameObject enemyToKill = new UnityEventGameObject();
    public class FreezeData { }
    public class UnityEventFreeze : UnityEvent<FreezeData> { }
    public UnityEventFreeze eventFreeze = new UnityEventFreeze();

    #endregion



    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
            Debug.LogError("MULTIPLE EVENT CORES IN SCENE");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



        // Use this for initialization
        void Start()
    {

        //[NOTE] is this good practise for events to nullcheck

        //if (testEvent == null)
        //{
        //    testEvent = new UnityEvent();
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }



    /// <summary>
    /// [FMOD EVENT TRIGGERS]
    /// This Section is for the event functions that are based on audio and talk to FMOD
    /// </summary>


    #region
    //player walking audio trigger

    //void AudioCall()
    //{
    //    if (Walking != null)
    //        Walking();
    //}

    //Tower Audio.

    //void TowerPower()
    //{
    //    if (audioTrigger != null)
    //    {
    //        //[HELP] will this work?
    //        audioTrigger();
    //    }
    //}

    //void Pyramid()
    //{
    //    if (audioTrigger != null)
    //    {
    //        //[HELP] will this work?
    //        audioTrigger();
    //    }
    //}

    #endregion



}
