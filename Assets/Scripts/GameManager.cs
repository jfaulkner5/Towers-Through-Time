using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentLevel;

    public static GameManager instance;

    private void Awake()
    {
        if  (instance != null)
        {
            DestroyImmediate(this);
            Debug.LogError("MULTIPLE GAMEMANAGERS IN SCENE");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
