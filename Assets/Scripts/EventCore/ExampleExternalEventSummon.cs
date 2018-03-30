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
            EventCore.Instance.externalEventTest.Invoke();
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
    }


}
