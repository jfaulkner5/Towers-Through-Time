using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SAE.WaveManagerTool;
using UnityEngine.AI;
using JSONSaveLoad;

public class TextLoading : MonoBehaviour
{
    string loadedFile = string.Empty;

    public Vector3 camStartPos;
    public float sideLength;
    Vector3 newPos = Vector3.zero;

    [HideInInspector]
    public int visualTheme;

    public GameObject[] blankTerrainTile;
    public GameObject[] MonumentTerrainTile;
    public GameObject[] playerTerrainTile;
    public GameObject[] towerTerainTower;
    public GameObject[] pathTerrainTile;
    public GameObject[] enemySpawnTerrainTile;
    public GameObject[] freezeObjectTerrainTile;
    public GameObject[] clutterGenObjects;

    public GameObject[] prehistoricBackground;
    public GameObject[] futuristicBackground;
    public GameObject[] apocalypticBackground;

    private void Start()
    {
        ReadFile();
        LoadLevel();
        LoadNavMesh();
        WaveSpawner.instance.Initialize();
        GameManager.instance.visualTheme = visualTheme;
    }

    public NavMeshSurface surface;

    void LoadNavMesh()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void ReadFile()
    {
        string path;
        if (Application.isEditor)
        {
            path = Path.Combine(Application.streamingAssetsPath, "LevelASCIIMaps/Level" + GameManager.instance.currentLevel + ".txt");
        }
        else
        {
            path = Path.Combine(Application.streamingAssetsPath, "LevelASCIIMaps/Level" + GameManager.instance.currentLevel + ".txt");
        }

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
                    case '\n':
                        newPos = new Vector3(0, 0, newPos.z + sideLength);
                        break;
                    case '+':
                        Instantiate(blankTerrainTile[visualTheme], newPos, Quaternion.identity);
                        break;
                    case 'M':
                        Instantiate(MonumentTerrainTile[visualTheme], newPos, Quaternion.identity);
                        break;
                    case 'P':
                        Vector3 playerPos = new Vector3(newPos.x, newPos.y, newPos.z);
                        Instantiate(playerTerrainTile[visualTheme], playerPos, Quaternion.identity);
                        break;
                    case 'T':
                        Instantiate(towerTerainTower[visualTheme], newPos, Quaternion.identity);
                        break;
                    case 'X':
                        Instantiate(enemySpawnTerrainTile[visualTheme], newPos, Quaternion.identity);
                        break;
                    case 'F':
                        Instantiate(freezeObjectTerrainTile[visualTheme], newPos, Quaternion.identity);
                        break;
                    case '-':
                        Instantiate(pathTerrainTile[visualTheme], newPos, Quaternion.identity);
                        break;
                    case 'S':
                        Instantiate(clutterGenObjects[Random.Range(0,clutterGenObjects.Length)], newPos, Quaternion.identity);
                        break;
                    case ' ':
                        break;

                }
            }

            //[todo] background loading stuff 

        }
    }
}
