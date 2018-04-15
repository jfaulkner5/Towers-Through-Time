using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public FMOD.Studio.ParameterInstance level;
    public FMOD.Studio.ParameterInstance winState;
    public FMOD.Studio.ParameterInstance enemyCount;

    public FMOD.Studio.ParameterInstance indexChecker;


    public FMOD.Studio.EventInstance walkPlay;
    public FMOD.Studio.EventInstance towerOneShot, towerFire, freezeSound;

    public FMOD.Studio.EventInstance winSound, lossSound;
    public FMOD.Studio.EventInstance buttonPress;

    public FMOD.Studio.EventInstance bgm;

    private string tempstring;
    private int audioTheme;

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


    /// <summary>
    /// The different levels are set up to trigger on 
    ///Menu: 0-0.95
    ///Pre-His: 1-1.95
    ///Apoco: 2-2.95
    ///Future: 3-4 
    ///
    /// INDEX VALUES FOR PARAMTERS
    /// 
    /// Level:      0
    /// EnemyCount: 1
    ///     should be 0-25
    /// Win/loss:   2
    ///     if 0.1-1 = loss
    ///         1.1 - 2.0 = win
    /// </summary>

    // Use this for initialization
    void Start()
    {
        AddEvents();

        //[FIX] placeholder bgm
        bgm = FMODUnity.RuntimeManager.CreateInstance(backgroundMusic);
        bgm.setVolume(0.3f);
        bgm.start();
        level.setValue(0.5f);



    }


    // Update is called once per frame
    void Update()
    {
        IntensityChanger();
        
        if(Input.GetKey(KeyCode.Alpha1))
        {
            bgm.setParameterValueByIndex(0, 0.5f);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            bgm.setParameterValueByIndex(0, 1.5f);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            bgm.setParameterValueByIndex(0, 2.5f);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            bgm.setParameterValueByIndex(0, 3.5f);
        }


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

    public void SetAudioTheme(int arg0)
    {
        audioTheme = arg0;
        ThematicAudioChanger();
    }

    private void ThematicAudioChanger()
    {


        switch (audioTheme)
        {
            case 1:
                bgm.setParameterValueByIndex(0, 1.5f);
                break;
            case 2:
                bgm.setParameterValueByIndex(0, 2.5f);
                break;

            case 3:
                bgm.setParameterValueByIndex(0, 3.5f);
                break;
        }


        //print("current level theme is " + GameManager.instance.visualTheme);

    }

    private void IntensityChanger()
    {
        float totalEnemies = WinStateCheck.enemyList.Count;
        if (totalEnemies >= 25f)
        {
            totalEnemies = 25f;
        }
        else if (totalEnemies <= 0f)
        {
            totalEnemies = 1f;
        }
        bgm.setParameterValueByIndex(1, totalEnemies);
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
        bgm.setParameterValueByIndex(3, 1.1f);
        bgm.setParameterValueByIndex(3, 0.0f);
        //winSound = FMODUnity.RuntimeManager.CreateInstance(winRef);
        //winSound.start();

    }



    public void OnGameLoss()
    {
        bgm.setParameterValueByIndex(3, 0.5f);
        bgm.setParameterValueByIndex(3, 0f);
        //lossSound = FMODUnity.RuntimeManager.CreateInstance(lossRef);
        //lossSound.start();
    }

    public void OnButtonClick()
    {

        buttonPress = FMODUnity.RuntimeManager.CreateInstance(buttonRef);
        buttonPress.start();
    }
}
