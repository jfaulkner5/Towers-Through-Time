using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    private bool pause = false;

	// Use this for initialization
	void Start ()
    {
        PauseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

        if(pause)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if(!pause)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

	}
    public void Resume()
    {
        pause = false;
    }
    public void ReturnToMenu()
    {
        pause = false;
        SceneManager.LoadScene("MainMenu");
    }
}
