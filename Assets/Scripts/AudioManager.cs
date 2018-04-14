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


    /// <summary>
    /// The different levels are set up to trigger on 
    //Menu: 0-0.95
    //Pre-His: 1-1.95
    //Apoco: 2-2.95
    //Future: 3-4 
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


        if (bgm.getParameter("Win/Lose", out winState) != FMOD.RESULT.OK)
        {
            Debug.LogError("parameter not found on music event");
            return;
        }

        if (bgm.getParameter("Level", out level) != FMOD.RESULT.OK)
        {
            Debug.LogError("parameter not found on music event");
            return;
        }

        if (bgm.getParameter("Enemy Count", out enemyCount) != FMOD.RESULT.OK)
        {
            Debug.LogError("parameter not found on music event");
            return;
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        ThematicAudioChanger();
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

    private void ThematicAudioChanger()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            level.setValue(0.5f);
        }

        if (GameManager.instance.visualTheme == 1)
            level.setValue(1f);
        if (GameManager.instance.visualTheme == 2)
            level.setValue(2f);
        if (GameManager.instance.visualTheme == 3)
            level.setValue(3f);

        print("current level theme is " + GameManager.instance.visualTheme);

    }

    private void IntensityChanger()
    {

        int totalEnemies = WinStateCheck.enemyList.Count;
        if (totalEnemies >= 25)
        {
            totalEnemies = 25;
        }
        else if (totalEnemies <= 0)
        {
            totalEnemies = 1;
        }
        enemyCount.setValue(totalEnemies);
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
