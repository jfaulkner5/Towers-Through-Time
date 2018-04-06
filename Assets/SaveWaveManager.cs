using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONSaveLoad;
using SAE.WaveManagerTool;
public class SaveWaveManager : MonoBehaviour {

    string levelNum;

    public WaveManager waveManager;

    public void Save(int sizeMultiplier)
    {
        SaveLoadManager.SaveFile("Level" + sizeMultiplier + ".json", waveManager);
        Debug.Log("SAVED LEVEL " + sizeMultiplier);
    }

    public void TakeLevelNum(string input)
    {
        levelNum = input;
    }
}
