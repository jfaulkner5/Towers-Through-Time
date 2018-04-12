using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoading : MonoBehaviour {

    public int levelInput = 0;

    public void TakeInput(int input)
    {
        levelInput = input;
    }

    public void LoadLevel()
    {
        //Hack why
        print("LOOP");
        if (levelInput > 0 && levelInput <= 80)
        {
            print(levelInput);
            GameManager.instance.currentLevel = levelInput;
            // HACK VVVVVV
            SceneManager.LoadScene("MainScene");
        }
    }

    //used this to pass the level back to the loader and to "restart" the level
    public void LoadLevel(int levelReload)
    { 
        levelInput = levelReload;
        LoadLevel();

    }

    private void Update()
    {
    }

    public void LoadMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
