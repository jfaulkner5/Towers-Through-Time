using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCore : MonoBehaviour
{
    //Singleton of the Main event core
    private static EventCore _eventCore;
    public static EventCore Instance
    {
        get
        {
            if (_eventCore == null)
            {
                _eventCore = FindObjectOfType<EventCore>();

                if (_eventCore == null)
                {
                    Debug.LogError("[user defined error thrown] There is no Delegate Event Core");
                }
            }
            return _eventCore;
        }
    }




    private void Awake()
    {
        //double checking for duplicate singletons of this object
        if (_eventCore == null)
        {
            _eventCore = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
