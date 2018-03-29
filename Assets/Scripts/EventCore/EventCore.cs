using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Debug.LogError("[user defined error thrown] There is no Delegate Event Core");
                }
            }
            return _eventCore;
        }
    }

    //Game Over
    public delegate void GameCore();

    public static event GameCore PyramidAttack, PyramidDamage, PyramidRepair;


    //Audio events
    public delegate void AudioTrigger();
        
        //player audio
    public static event AudioTrigger Walking;
        //Tower Audio
    public static event AudioTrigger PowerUp, PowerDown, Shoot;

    //Enemy Spawner stuff
    public delegate void EnemySpawner();

    public static event EnemySpawner AttackPyramid, Death, AttackSound, DeathSound;





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

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This Section is for the event functions that are based on audio and talk to FMOD
    /// </summary>
    /// 

    //player walking audio trigger

    void AudioCall()
    {
        if (Walking != null)
            Walking();
    }

    //Tower Audio.

    void TowerPower(AudioTrigger audioTrigger)
    {
        if(audioTrigger != null)
        {
            //[HELP] will this work?
            audioTrigger();
        }
    }

    void Pyramid(AudioTrigger audioTrigger)
    {
        if (audioTrigger != null)
        {
            //[HELP] will this work?
            audioTrigger();
        }
    }

}
