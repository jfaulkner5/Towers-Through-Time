using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PowerOn()
    {

    }

    void PowerOff()
    {

    }

    public IEnumerator WaitTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}


