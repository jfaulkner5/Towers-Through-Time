using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODtest : MonoBehaviour
{

    [FMODUnity.EventRef] public string walkAudio;

    public FMOD.Studio.EventInstance walkPlay;

    // Use this for initialization
    void Start()
    {
        walkPlay = FMODUnity.RuntimeManager.CreateInstance(walkAudio);
        walkPlay.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            walkPlay.start();
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            walkPlay.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
