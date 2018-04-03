using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleExternalEventSummon : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            EventCore.Instance.towerFire.Invoke();
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            EventCore.Instance.towerFireStop.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
    }


}
