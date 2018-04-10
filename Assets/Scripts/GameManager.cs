using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currentLevel;
    public float totalLevels;
    public int visualTheme;


    public Dictionary<float, bool> levelsBeat = new Dictionary<float, bool>();


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

        for (int index = 1; index < totalLevels + 1; index++)
        {
            instance.levelsBeat.Add(index, false);
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
