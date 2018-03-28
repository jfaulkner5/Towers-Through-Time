using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [FMODUnity.EventRef] public string walkAudio;

    [FMODUnity.EventRef] public string towerPowerUp, towerPowerDown, towerShoot;


    public FMOD.Studio.EventInstance walkPlay;
    public FMOD.Studio.EventInstance towerOneShot, towerFire;

    private string tempstring;

    // Use this for initialization
    void Start()
    {
        //walkPlay = FMODUnity.RuntimeManager.CreateInstance(walkAudio);
        //walkPlay.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TowerBoot(true);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            towerOneShot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        if(Input.GetKeyDown(KeyCode.Keypad3))
        {

            TowerFire(true);

        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

            TowerFire(false);

        }
    }


    //tower related audio
    public void TowerBoot(bool powerState)
    {
        if (powerState)
        {
            tempstring = towerPowerUp;
        }
        else
        {
            tempstring = towerPowerDown;
        }
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

}
