using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currentLevel;
    public float totalLevels;
    public int visualTheme;

    public static GameManager instance;

    EventCore.WinData winData = new EventCore.WinData();

    private void Awake()
    {
        if (instance != null)
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

    private void Start()
    {
        //EventCore.Instance.levelWon.AddListener(GameWon);
  
    }

    void GameWon(EventCore.WinData winData)
    {
        //SceneManager.LoadScene("Menu
    }
}
