using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoading : MonoBehaviour {

    public int levelInput = 0;

    public void TakeInput(string input)
    {
        levelInput = int.Parse(input);
    }

    public void LoadLevel()
    {
        print("LOOP");
        if (levelInput > 0 && levelInput <= 80)
        {
            print(levelInput);
            GameManager.instance.currentLevel = levelInput;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void LoadMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
