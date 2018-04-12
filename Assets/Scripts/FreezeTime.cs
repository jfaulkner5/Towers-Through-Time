using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {



    public ParticleSystem freezeTime_PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool doFreeze;
            FreezeManager.instance.CheckForFreeze(out doFreeze);
            if (doFreeze)
            {
                freezeTime_PS.Play();
            }
        }
    }



}
