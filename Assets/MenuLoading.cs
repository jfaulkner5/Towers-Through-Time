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

    //used this to pass the level back to the loader and to "restart" the level
    public void LoadLevel(int levelReload)
    { 
        levelInput = levelReload;
        LoadLevel();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadLevel();
        }
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
