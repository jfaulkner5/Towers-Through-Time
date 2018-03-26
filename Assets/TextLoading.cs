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

    private void Start()
    {
        ReadFile();
        LoadLevel();
    }

    void ReadFile()
    {
        string path = "Assets/Resources/file.txt";

        //Read the text from directly from the file.txt file
        StreamReader reader = new StreamReader(path);
        loadedFile = reader.ReadToEnd();
        Debug.Log(loadedFile);
        reader.Close();
    }

    void LoadLevel()
    {
        if (loadedFile != string.Empty)
        {
            for (int index = 0; index < loadedFile.Length; index++)
            {
                newPos += new Vector3(sideLength, 0, 0);
                switch (loadedFile[index])
                {
                    case '\n':
                        newPos = new Vector3(0, 0, newPos.z + sideLength);
                        break;
                    case '|':
                        Instantiate(objectsToLoad[0],newPos,transform.rotation);
                        break;
                    case '+':
                        Instantiate(objectsToLoad[1],newPos,transform.rotation);
                        break;
                }
            }
        }
        cam.transform.position = camStartPos;
        cam.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
