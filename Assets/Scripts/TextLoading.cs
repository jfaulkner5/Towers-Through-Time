using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextLoading : MonoBehaviour
{
    string loadedFile = string.Empty;

    public Vector3 camStartPos;
    public float sideLength;
    Vector3 newPos = Vector3.zero;
    public Camera cam;

    [HideInInspector]
    public int visualTheme;

    public GameObject[] blankTerrainTile;
    public GameObject[] MonumentTerrainTile;
    public GameObject[] playerTerrainTile;
    public GameObject[] towerTerainTower;
    public GameObject[] pathTerrainTile;
    public GameObject[] enemySpawnTerrainTile;
    public GameObject[] freezeObjectTerrainTile;

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
                    visualTheme = 0;
                    break;
                case '2':
                    visualTheme = 1;
                    break;
                case '3':
                    visualTheme = 2;
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
                    case '+':
                        Instantiate(blankTerrainTile[visualTheme], newPos, transform.rotation);
                        break;
                    //path object
                    case 'M':
                        Instantiate(MonumentTerrainTile[visualTheme], newPos, transform.rotation);
                        break;
                    case 'P':
                        Vector3 playerPos = new Vector3(newPos.x, newPos.y + 1, newPos.z);
                        Instantiate(playerTerrainTile[visualTheme], playerPos, transform.rotation);
                        break;
                    //path object
                    case 'T':
                        Instantiate(towerTerainTower[visualTheme], newPos, transform.rotation);
                        break;
                    //path object
                    case 'X':
                        Instantiate(enemySpawnTerrainTile[visualTheme], newPos, transform.rotation);
                        break;
                    //path object
                    case 'F':
                        Instantiate(freezeObjectTerrainTile[visualTheme], newPos, transform.rotation);
                        break;
                    //path object
                    case '-':
                        Instantiate(pathTerrainTile[visualTheme], newPos, transform.rotation);
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
