using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[HELP] are other scripts dependant on the events system if EVENT CORE is an instance.
//It would appear not.
//using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;
    public static AudioManager Instance
    {
        get
        {
            if (_audioManager == null)
            {
                _audioManager = FindObjectOfType<AudioManager>();

                if (_audioManager == null)
                {
                    Debug.LogError("[user defined error thrown] There is no Delegate Event Core");
                }
            }
            return _audioManager;
        }
    }

    [FMODUnity.EventRef] public string walkAudio;
    [FMODUnity.EventRef] public string towerPowerUp, towerPowerDown, towerShoot;

    public FMOD.Studio.EventInstance walkPlay;
    public FMOD.Studio.EventInstance towerOneShot, towerFire;

    private string tempstring;

    // Use this for initialization
    void Start()
    {
        AddEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddEvents()
    {
        EventCore.Instance.externalEventTest.AddListener(TestFunc);
    }


    //tower related audio
    #region
    public void TowerBoot()
    {

        tempstring = towerPowerDown;

        towerOneShot = FMODUnity.RuntimeManager.CreateInstance(tempstring);
        towerOneShot.start();
    }

    public void TowerFire(bool isFiring)
    {
        if (isFiring)
        {
            towerFire = FMODUnity.RuntimeManager.CreateInstance(towerShoot);
            towerFire.start();
        }
        else
        {
            towerFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }

    #endregion

    public void TestFunc()
    {
        Debug.Log("Event reached func");
    }

    
}
