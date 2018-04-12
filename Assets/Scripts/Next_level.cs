using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_level : MonoBehaviour
{
    public MenuLoading menuLoading;

    public int currentLevel;
    public int levelLoading;

    public GameObject menuPanel;

    // Use this for initialization
    void Start()
    {
        EventCore.Instance.levelWon.AddListener(GameWon);
        EventCore.Instance.levelLost.AddListener(GameLoss);


        if (menuLoading == null)
        {
            print("menuloading wasn't found");
        }

        currentLevel = GameManager.instance.currentLevel;
    }

    public void OnClickRestart()
    {
        //function for restart click
        RestartLevel();
    }

    public void OnClickNext()
    {
        //Next level button click
        NextLevel();
    }

    public void OnClickMenu()
    {
        MenuLoad();
    }

    private void RestartLevel()
    {
        //restart level code
        levelLoading = currentLevel;
        Time.timeScale = 1;

    }

    private void NextLevel()
    {
        Time.timeScale = 1;
        levelLoading = currentLevel;
        levelLoading++;
    }

    private void MenuLoad()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameWon()
    {
        print("LEVEL WON | end level trigger");
        //Time.timeScale = 0;

    }

    public void GameLoss()
    {
        print("LEVEL LOST | end level start");
        //Time.timeScale = 0;
    }


}



