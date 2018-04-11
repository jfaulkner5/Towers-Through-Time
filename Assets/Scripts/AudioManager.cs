using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[HELP] are other scripts dependant on the events system if EVENT CORE is an instance.
//It would appear not.
//using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;
    public static AudioManager Instance;


    [FMODUnity.EventRef] public string walkAudio;
    [FMODUnity.EventRef] public string towerPowerUp, towerPowerDown, towerShoot, freezeRef;
    [FMODUnity.EventRef] public string winRef, lossRef;
    [FMODUnity.EventRef] public string buttonRef;
    [FMODUnity.EventRef] public string backgroundMusic;




    public FMOD.Studio.EventInstance walkPlay;
    public FMOD.Studio.EventInstance towerOneShot, towerFire, freezeSound;

    public FMOD.Studio.EventInstance winSound, lossSound;
    public FMOD.Studio.EventInstance buttonPress;

    public FMOD.Studio.EventInstance bgm;

    private string tempstring;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        AddEvents();

        //[FIX] placeholder bgm
        bgm = FMODUnity.RuntimeManager.CreateInstance(backgroundMusic);
        bgm.setVolume(0.2f);
        bgm.start();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddEvents()
    {
        //EventCore.Instance.externalEventTest.AddListener(TestFunc);
        EventCore.Instance.towerOn.AddListener(TowerPowerUpAudio);
        EventCore.Instance.towerOff.AddListener(TowerPowerDownAudio);
        EventCore.Instance.towerFire.AddListener(TowerFire);
        EventCore.Instance.playerWalk.AddListener(PlayerWalk);
        EventCore.Instance.levelWon.AddListener(OnGameWin);
        EventCore.Instance.levelLost.AddListener(OnGameLoss);

        EventCore.Instance.eventFreeze.AddListener(TowerFreeze);

    }




    //tower related audio
    #region
    public void TowerPowerUpAudio()
    {
        towerOneShot = FMODUnity.RuntimeManager.CreateInstance(towerPowerUp);
        towerOneShot.start();
    }

    public void TowerPowerDownAudio()
    {
        towerOneShot = FMODUnity.RuntimeManager.CreateInstance(towerPowerDown);
        towerOneShot.start();
    }

    public void TowerFire()
    {

        towerFire = FMODUnity.RuntimeManager.CreateInstance(towerShoot);
        towerFire.start();

    }

    public void PlayerWalk()
    {
        walkPlay = FMODUnity.RuntimeManager.CreateInstance(walkAudio);

    }

    public void TowerFreeze(EventCore.FreezeData arg0)
    {
        freezeSound = FMODUnity.RuntimeManager.CreateInstance(freezeRef);
        freezeSound.start();
    }

    #endregion

    public void OnGameWin()
    {
        winSound = FMODUnity.RuntimeManager.CreateInstance(winRef);
        winSound.start();

    }



    public void OnGameLoss()
    {
        lossSound = FMODUnity.RuntimeManager.CreateInstance(lossRef);
        lossSound.start();
    }

    public void OnButtonClick()
    {
        
        buttonPress = FMODUnity.RuntimeManager.CreateInstance(buttonRef);
        buttonPress.start();
    }
}
