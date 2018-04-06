using UnityEngine;
using UnityEditor;
using JSONSaveLoad;
using SAE.WaveManagerTool;

public class EditModeFunctions : EditorWindow
{
    public WaveManager waveManager;

    int clones;
    static int sizeMultiplier;

    [MenuItem("Window/Save Wave Data")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Save Wave Data");
    }

    private void OnGUI()
    {
        sizeMultiplier = EditorGUILayout.IntField("Level To Save:", sizeMultiplier);
        if (GUILayout.Button("Save Wave Data"))
        {
            FunctionToRun();
        }
    }

    private void FunctionToRun()
    {
        FindObjectOfType<SaveWaveManager>().Save(sizeMultiplier);

    }
}