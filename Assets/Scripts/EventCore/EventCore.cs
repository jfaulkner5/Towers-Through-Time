using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    //Singleton of the Main event core
    private static EventCore _eventCore;
    public static EventCore Instance
    {
        get
        {
            if (_eventCore == null)
            {
                _eventCore = FindObjectOfType<EventCore>();

                if (_eventCore == null)
                {
                    Debug.LogError("[user defined error thrown] There is no Delegate Event Core ");
                }
            }
            return _eventCore;
        }
    }




    //ADD NEW EVENTS HERE PLEASE
    #region

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

    //Enemy Spawner stuff
    #region

    public UnityEvent enemySpawn, enemyDeath;

    #endregion

    //Tower stuff
    #region

        public class UnityEventGameObject : UnityEvent<GameObject> { }
    public UnityEventGameObject enemyToKill = new UnityEventGameObject();

    #endregion



    private void Awake()
    {
        //double checking for duplicate singletons of this object
        if (_eventCore == null)
        {
            _eventCore = this;
        }
        else
        {
            DestroyImmediate(gameObject);
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

    void TestCall()
    {
        AudioManager.Instance.TestFunc();
    }

}
