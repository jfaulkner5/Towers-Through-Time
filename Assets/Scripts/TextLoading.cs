using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextLoading : MonoBehaviour
{
    string loadedFile = string.Empty;

    public Vector3 camStartPos;
    public GameObject[] objectsToLoad;
    public float sideLength;
    Vector3 newPos = Vector3.zero;
    public Camera cam;
    public GameObject player;

    private void Start()
    {
        ReadFile();
        LoadLevel();
    }

    void ReadFile()
    {
        string path = Path.Combine(Application.dataPath, "LevelASCIIMaps\\Level" + GameManager.instance.currentLevel + ".txt");

        //Read the text from directly from the file.txt file
        StreamReader reader = new StreamReader(path);
        loadedFile = reader.ReadToEnd();
        reader.Close();
    }

    void LoadLevel()
    {
        if (loadedFile != string.Empty)
        {
            switch (loadedFile[0])
            {
                case '1':
                    SetVisuals(0);
                    break;
                case '2':
                    SetVisuals(1);
                    break;
                case '3':
                    SetVisuals(2);
                    break;
            }

            for (int index = 2; index < loadedFile.Length; index++)
            {
                newPos += new Vector3(sideLength, 0, 0);
                switch (loadedFile[index])
                {
                    //new line of objects
                    case '\n':
                        newPos = new Vector3(0, 0, newPos.z + sideLength);
                        break;
                    //base object
                    case '|':
                        Instantiate(objectsToLoad[0], newPos, transform.rotation);
                        break;
                    //path object
                    case '+':
                        Instantiate(objectsToLoad[1], newPos, transform.rotation);
                        break;
                    case 'P':
                        Vector3 playerPos = new Vector3(newPos.x, newPos.y + 1, newPos.z);
                        Instantiate(player, playerPos, transform.rotation);
                        break;
                    //empty space
                    case ' ':
                        break;
                }
            }
        }
        cam.transform.position = camStartPos;
        cam.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    void SetVisuals(int visualStyle)
    {

    }
}
