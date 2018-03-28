using UnityEngine;
using UnityEditor;
using System.IO;
using SAE.WaveManagerTool;

/// <summary>
/// This is the custom editor script for the Wavemanager.
/// 
/// This script creates a custom editor GUI to help make the Wavemanager easier to use.
/// </summary>

[CustomEditor(typeof(WaveManager))]
[CanEditMultipleObjects]
public class WaveManagerEditor : Editor
{

    SerializedProperty spawnChance;
    SerializedProperty itemPrefab;
    SerializedProperty waitBeforeSpawn;
    SerializedObject obj;

    int _tempInt;
    Object _tempObject;

    Transform[] sPoints;

    void OnEnable()
    {

        //ob = new SerializedObject(target);
        SerializedProperty child = serializedObject.FindProperty("spawnObject");

        if (child == null || child.objectReferenceValue == null)
        {
            Debug.Log("No spawn asset found");
        }
        else
        {
            obj = new SerializedObject(child.objectReferenceValue);

            //Debug.Log(obj.GetType ().Name);

            //Error handling
            spawnChance = obj.FindProperty("spawnChance");
            if (!spawnChance.isArray)
            {
                // You shouldn't see this.
                Debug.LogError("Property is not an array!");
            }

            itemPrefab = obj.FindProperty("itemPrefab");
            if (!itemPrefab.isArray)
            {
                // You shouldn't see this.
                Debug.LogError("Property is not an array!");
            }

            waitBeforeSpawn = obj.FindProperty("waitBeforeSpawn");
            if (!waitBeforeSpawn.isArray)
            {
                // You shouldn't see this.
                Debug.LogError("Property is not an array!");
            }

        }
        try
        {
            if (Selection.activeGameObject != null)
            {
                if (Selection.activeTransform)
                {
                    WaveManager scr = Selection.activeGameObject.GetComponent<WaveManager>();
                    if (scr != null)
                    {
                        scr.GetSpawnPoints();
                        sPoints = scr.spawnPoints;

                        if (sPoints.Length == 0)
                        {
                            Debug.LogWarning("No spawnpoints found, creating default spawn point");
                            CreateSpawnPoint(scr);
                        }
                    }
                }
                else
                {
                    Debug.Log("Editing SmartSpawn in project hierarchy");
                }

            }


        }
        catch (UnityException e)
        {
            if (e == null)
                Debug.Log("Null exception");
        }

    }

    /// <summary>
    /// Creates a new gameobject with the name spawnpoint and either copies componet from exisitng spawn point or creates a new SpawnpointController component.
    /// </summary>
    /// <param name="_waveManager"></param>
    public void CreateSpawnPoint(WaveManager _waveManager)
    {
        GameObject g = new GameObject();
        g.transform.position = (Selection.activeTransform.position + Random.insideUnitSphere * 5f);
        g.transform.rotation = Selection.activeTransform.rotation;
        g.transform.parent = Selection.activeTransform;

        //If the spawner already has a spawnpoint, make sure we use the references from that spawnpoints SpawnpointController
        if (_waveManager.spawnPoints.Length > 0)
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(_waveManager.spawnPoints[0].GetComponent<SpawnPointController>());
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(g);

            g.layer = _waveManager.spawnPoints[0].gameObject.layer;
        }
        else
        {
            //Add the SpawnPointController script to it
            g.AddComponent(typeof(SpawnPointController));
        }

        //HACK
        //Add Icon to it
        //Texture2D icon = (Texture2D)Resources.Load("CustomIcon");
        //var editorGUIUtilityType = typeof(EditorGUIUtility);
        //var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        //var args = new object[] { g, icon };
        //editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);

        // Use iconmanager extention to add icon to new spawnpoints instead of above rubbish
        IconManagerExtension.SetIcon(g, IconManager.LabelIcon.Blue);

        string n = ("SpawnPoint_" + (sPoints.Length + 1).ToString());
        g.name = n;
        Undo.RegisterCreatedObjectUndo(g, n);
        this.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        //Updates the object we are editing
        serializedObject.Update();

        //References for when we want to change the size of the array
        bool enlarge = false;
        bool decrease = false;

        //Reference to the WaveManager
        WaveManager s = target as WaveManager;

        if (itemPrefab == null || spawnChance == null || waitBeforeSpawn == null)
        {
            this.OnEnable();
        }

        #region UIStatus
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.BeginHorizontal();

        //Spawn timer (how long between spawns)
        EditorGUILayout.LabelField("Next spawn in: " + ((int)(s.totalSpawnCountdown - s.timer)).ToString(), EditorStyles.boldLabel, GUILayout.Width(150f));

        //Button to manually call the spawn function, only if the game is in play mode
        if (GUILayout.Button("Start Spawn"))
        {

            if (Application.isPlaying)
            {
                s.Spawn();
            }

            if (!Application.isPlaying)
            {
                Debug.Log("You can only call the spawn function when the game is running");
            }

        }

        //Button to stop the spawn function, only if the game is in play mode
        if (GUILayout.Button("Stop Spawn"))
        {

            if (Application.isPlaying)
            {
                s.Spawn();
            }

            if (!Application.isPlaying)
            {
                Debug.Log("You can only call the spawn function when the game is running");
            }

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        #endregion

        #region Settings

        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.LabelField("Settings:", EditorStyles.boldLabel);


        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        //Checkbox for wave spawning
        EditorGUILayout.LabelField("Use Wave Spawning", GUILayout.Width(163f));

        EditorGUI.BeginChangeCheck();
        bool bWave = EditorGUILayout.Toggle(s.useWaveSpawning);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Use Wave Spawning");
            s.useWaveSpawning = bWave;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Time between spawns", GUILayout.Width(150f));

        EditorGUI.BeginChangeCheck();

        float iTwo = EditorGUILayout.FloatField(s.spawnCountdown, GUILayout.Width(40f));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change time between spawns");
            s.spawnCountdown = iTwo;
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndHorizontal();


        if (s.useWaveSpawning == true)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Reset waves after time?", GUILayout.Width(163f));

            EditorGUI.BeginChangeCheck();
            bool bWaveT = EditorGUILayout.Toggle(s.resetWaveValuesAfterTime);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Reset waves after time?");
                s.resetWaveValuesAfterTime = bWaveT;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (s.resetWaveValuesAfterTime == true && s.useWaveSpawning == true)
        {

            EditorGUILayout.LabelField("Wave reset time", GUILayout.Width(150f));

            EditorGUI.BeginChangeCheck();
            float rOne = EditorGUILayout.FloatField(s.waveResetTime, GUILayout.Width(40f));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Wave resettime");
                s.waveResetTime = rOne;
            }

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        //Enables a maximum amount of spawns
        EditorGUILayout.LabelField("Cap maximum spawns", GUILayout.Width(163f));

        EditorGUI.BeginChangeCheck();
        bool bTwo = EditorGUILayout.Toggle(s.useMaxSpawns);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Use max spawns");
            s.useMaxSpawns = bTwo;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (s.useMaxSpawns)
        {
            EditorGUILayout.LabelField("Max Spawns", GUILayout.Width(150f));

            EditorGUI.BeginChangeCheck();
            int iOne = EditorGUILayout.IntField(s.maxSpawnerSpawns, GUILayout.Width(40f));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change max spawns");
                s.maxSpawnerSpawns = iOne;
            }

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        #endregion

        #region Wave Details

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Wave Details:", EditorStyles.boldLabel);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();

        EditorGUI.BeginChangeCheck();
        WaveScriptableObject spOne = (WaveScriptableObject)EditorGUILayout.ObjectField("Wave Data File", s.spawnObject, typeof(WaveScriptableObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Wave Data");
            s.spawnObject = spOne;
        }

        WaveScriptableObject targetSpawnObject = s.spawnObject;

        EditorGUILayout.EndVertical();




        if (GUILayout.Button("New", GUILayout.Width(50f)))
        {
            CreateWaveDataAsset();
        }


        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Note: Values changed in playmode are not saved");
        }

        if (targetSpawnObject != null)
        {
            //Labels
            if (s.useWaveSpawning == true)
            {
                EditorGUILayout.LabelField("Amount & Wait Time", "Object to spawn");
            }
            else
            {
                EditorGUILayout.LabelField("Spawn Chance & Wait Time", "Object to spawn");
            }

            //Start the horizontal (2 per line) GUI layour
            EditorGUILayout.BeginHorizontal();

            if (targetSpawnObject.spawnChance != null)
            {

                //Make a column of the spawn chance ratio sliders
                EditorGUILayout.BeginVertical();

                if (Application.isPlaying)
                {
                    for (int i = 0; i < s.spawnChances.Count; ++i)
                    {
                        s.spawnChances[i] = EditorGUILayout.IntField(s.spawnChances[i], GUILayout.MaxWidth(50f));
                    }
                }
                else
                {
                    for (int i = 0; i < targetSpawnObject.spawnChance.Length; ++i)
                    {
                        EditorGUI.BeginChangeCheck();
                        int iThree = EditorGUILayout.IntField(s.spawnObject.spawnChance[i], GUILayout.MaxWidth(50f));
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(obj.targetObject, "Change spawn chance");
                            s.spawnObject.spawnChance[i] = iThree;
                        }

                    }
                }
                EditorGUILayout.EndVertical();


                ////Make a column of the Wait time values
                EditorGUILayout.BeginVertical();

                if (Application.isPlaying)
                {
                    for (int i = 0; i < s.spawnWaitTimes.Count; ++i)
                    {
                        s.spawnWaitTimes[i] = EditorGUILayout.FloatField(s.spawnWaitTimes[i], GUILayout.MaxWidth(50f));
                    }
                }
                else
                {
                    for (int i = 0; i < targetSpawnObject.waitBeforeSpawn.Length; ++i)
                    {
                        EditorGUI.BeginChangeCheck();
                        float iFour = EditorGUILayout.FloatField(s.spawnObject.waitBeforeSpawn[i], GUILayout.MaxWidth(50f));
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(obj.targetObject, "Change wait time");
                            s.spawnObject.waitBeforeSpawn[i] = iFour;
                        }

                    }
                }
                EditorGUILayout.EndVertical();


                //Make a column of the item prefab pickers
                EditorGUILayout.BeginVertical();

                if (Application.isPlaying)
                {
                    for (int n = 0; n < s.objectPrefabs.Count; ++n)
                    {
                        s.objectPrefabs[n] = EditorGUILayout.ObjectField(s.objectPrefabs[n], typeof(GameObject), false);
                    }
                }
                else
                {
                    for (int n = 0; n < targetSpawnObject.spawnChance.Length; ++n)
                    {

                        EditorGUI.BeginChangeCheck();
                        Object pOne = EditorGUILayout.ObjectField(s.spawnObject.itemPrefab[n], typeof(GameObject), false);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(obj.targetObject, "Change spawn obj");
                            s.spawnObject.itemPrefab[n] = pOne;
                        }
                    }
                }
                EditorGUILayout.EndVertical();

            }



            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.Space();

        //Plus button
        if (!Application.isPlaying)
        {
            if (GUILayout.Button("Add", GUILayout.Width(60f)))
            {
                enlarge = true;
            }

            //Minus button
            if (GUILayout.Button("Remove", GUILayout.Width(60f)))
            {
                decrease = true;
            }

        }

        //If we press the plus button, increase the size of the array
        if (enlarge)
        {
            EnlargeArray();
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();

        //If we press the minus button, decrease array size
        if (decrease)
        {
            DecreaseArray();
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        #endregion

        #region Spawn Point Details

        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.LabelField("Spawn Point Details:", EditorStyles.boldLabel);


        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.Space();
        if (GUILayout.Button("New Spawn Point +"))
        {
            CreateSpawnPoint(s);
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Name", GUILayout.Width(100f));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Location");
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Enabled", GUILayout.Width(100f));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Remove");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        for (int n = 0; n < sPoints.Length; ++n)
        {

            EditorGUILayout.LabelField(sPoints[n].name, GUILayout.Width(100f));
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        for (int n = 0; n < sPoints.Length; ++n)
        {

            EditorGUILayout.Vector3Field("", sPoints[n].position);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        for (int n = 0; n < sPoints.Length; ++n)
        {

            //Enables a maximum amount of spawns
            EditorGUI.BeginChangeCheck();
            bool bThree = EditorGUILayout.Toggle(sPoints[n].GetComponent<SpawnPointController>().spawnEnabled);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Enable Spawn point");
                sPoints[n].GetComponent<SpawnPointController>().spawnEnabled = bThree;
            }

        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical();
        for (int n = 0; n < sPoints.Length; ++n)
        {

            if (!Application.isPlaying)
            {
                //Minus button
                if (GUILayout.Button("-", GUILayout.Width(30f), GUILayout.Height(15f)))
                {
                    //TODO
                    // Do Remove
                }

            }
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        #endregion


        if (GUI.changed)
        {
            this.OnEnable();

            EditorUtility.SetDirty(target);

            if (s.spawnObject != null)
            {
                EditorUtility.SetDirty(s.spawnObject);
            }
        }

    }

    //Increase the size of the arrays
    void EnlargeArray()
    {
        int enlarged = spawnChance.arraySize;
        int itemEnlarged = itemPrefab.arraySize;
        int waitTimeEnlarged = waitBeforeSpawn.arraySize;
        spawnChance.InsertArrayElementAtIndex(enlarged);
        itemPrefab.InsertArrayElementAtIndex(itemEnlarged);
        waitBeforeSpawn.InsertArrayElementAtIndex(waitTimeEnlarged);
        obj.ApplyModifiedProperties();

    }

    //Decrease the size of the arrays
    void DecreaseArray()
    {
        spawnChance.arraySize--;
        itemPrefab.arraySize = spawnChance.arraySize;
        waitBeforeSpawn.arraySize = spawnChance.arraySize;
        obj.ApplyModifiedProperties();

    }


    // currently not used
    void ModifyWaveDataAsset(Object[] items, int[] chances, WaveScriptableObject origAsset)
    {
        origAsset.itemPrefab = items;
        origAsset.spawnChance = chances;

        AssetDatabase.SaveAssets();
    }

    void OnSceneGUI()
    {
        if (sPoints.Length == 0) OnEnable();

        foreach (Transform t in sPoints)
        {
            t.position = Handles.PositionHandle(t.position, t.rotation);
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }


    }

    /// <summary>
    /// Used to create a new wave data object
    /// </summary>
    [MenuItem("Assets/Create/Wave Data Asset")]
    public static void CreateWaveDataAsset()
    {
        WaveScriptableObject asset = ScriptableObject.CreateInstance<WaveScriptableObject>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/WaveData/NewWaveDataAsset.Asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;

    }

}
