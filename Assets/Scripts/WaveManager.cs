using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is used to spawn Objects in random spawn positions.
/// 
/// It accesses the SpawnpointController script to see whether 
/// there the spawm point is enabled.
/// 
/// </summary>

namespace SAE.WaveManagerTool
{

    public class WaveManager : MonoBehaviour
    {
        #region Variables

        public delegate void SpawnAction();
        /// <summary>
        /// Occurs when an item is spawned.
        /// </summary>
        public static event SpawnAction OnSpawn;

        public delegate void WaveResetAction(WaveManager spawnerWithWaveReset, int wave);
        /// <summary>
        /// Occurs when a new wave is triggered.
        /// </summary>
        public static event WaveResetAction OnWaveReset;

        /// <summary>
        /// Should we check if the SpawnPointController script has spawns left
        /// (maxSpawnsLeft) before spawning?
        /// </summary>
        public bool useMaxPointSpawns;

        /// <summary>
        /// The maximum amount of spawns that this spawner can spawn, if useMaxSpawns is true
        /// </summary>
        public int maxSpawnerSpawns;

        /// <summary>
        /// Should we check if this spawner is under the maximum spawn amount 
        /// <param name="maxSpawnerSpawns">Max spawn amount</param> before spawning?
        /// </summary>
        public bool useMaxSpawns;

        //The array of potential spawning locations
        public Transform[] spawnPoints;
        private SpawnPointController[] spawnChecks;

        //The final spawn location
        private Transform spawnLocation;

        /// <summary>
        /// The scriptable wave spawn asset that the spawner uses to reference objects & their chances/amounts
        /// </summary>
        public WaveScriptableObject spawnObject;

        //The item we will spawn
        public List<Object> /*Object[]*/ objectPrefabs;
        //The spawn chance of the item.
        public List<int> /*int[]*/ spawnChances;

        public List<float> /*float[]*/ spawnWaitTimes;

        //Spawn countdown timer
        public float timer = 0.0f;
        public float waveTimer = 0.0f;

        //The time between each spawn
        public float spawnCountdown;
        //Adjusted time between each spawn
        public float totalSpawnCountdown;

        /// <summary>
        /// Eanables wave spawning, uses wave data file to produce wave.
        /// </summary>
        public bool useWaveSpawning = false;

        int currentWave = 1;

        /// <summary>
        /// Can be used to create a timer that will reet the waves values after a given amount of time
        /// </summary>
        public bool resetWaveValuesAfterTime = false;

        /// <summary>
        /// After this amount of time, the WaveScriptableObject used to spawn the wave will be reset
        /// </summary>
        public float waveResetTime = 30f;

        //The summed up spawn ratios of the items
        private int totalRatio;

        //Reference to the SpawnpointController script
        private SpawnPointController spawnController;

        #endregion

        /// <summary>
        /// Gets the spawn point transforms and adds them to a list. Performs a check to see if the spawnpoints contain the SpawnPointController Script.
        /// </summary>
        public void GetSpawnPoints()
        {
            
            //Fill the spawn points array with all possible spawn points
            List<Transform> sP = new List<Transform>();

            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (Transform t in transform)
            {
                sP.Add(t);
            }

            if (sP.Count != spawners.Length)
            {
                float toAdd = spawners.Length - sP.Count;
                for (int index = 0; index < toAdd; index++)
                {
                    GameObject GO = new GameObject();
                    GO.transform.parent = transform;
                    GO.AddComponent<SpawnPointController>();
                    sP.Add(GO.transform);
                }
            }

            spawnPoints = sP.ToArray();

            for (int index = 0; index < spawners.Length; index++)
            {
                spawnPoints[index].position = spawners[index].transform.position;
                spawnPoints[index].position = new Vector3(spawnPoints[index].position.x, spawnPoints[index].position.y + 1, spawnPoints[index].position.z);
            }

            spawnChecks = GetComponentsInChildren<SpawnPointController>();

            

            if (spawnChecks.Length != spawnPoints.Length)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Ignore this message if clicking on a WaveManager in the project hierarchy");
#endif
                Debug.LogError("Warning - make sure all child gameobjects have one SpawnPointController script on them - " +
                               spawnChecks.Length + " SpawnPointControllers found, " + spawnPoints.Length + " SpawnPoints (first-level child gameobjects) found");
                Debug.LogError("Please remove any child gameobjects from the WaveManager that are not SpawnPoints.");
            }
        }
        
        void Awake()
        {
            ResetItemLists();
        }

        /// <summary>
        /// Resets the item lists to the initial state 
        /// </summary>
        public void ResetItemLists()
        {
            SetObjectLists(spawnObject);
        }

        /// <summary>
        /// Setups the object lists.
        /// </summary>
        public void SetObjectLists(WaveScriptableObject wso)
        {
            objectPrefabs.Clear();
            spawnChances.Clear();
            spawnWaitTimes.Clear();

            objectPrefabs.AddRange(wso.itemPrefab);
            spawnChances.AddRange(wso.spawnChance);
            spawnWaitTimes.AddRange(wso.waitBeforeSpawn);
        }



        public void Initialize()
        {
            GetReferences();

            GetSpawnPoints();

            // Adds the wait time of the next object to the spawncountdown
            totalSpawnCountdown = spawnCountdown + spawnObject.waitBeforeSpawn[0];
        }

        /// <summary>
        /// Called on Start()
        /// </summary>
        protected virtual void GetReferences()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (maxSpawnerSpawns <= 0 && useMaxSpawns == true) return;

            // TODO
            // Check if there are any objects left to spawn and return if no objects left


            if (useWaveSpawning == true && resetWaveValuesAfterTime == true)
            {
                waveTimer += Time.deltaTime;

                if (waveTimer > waveResetTime)
                {
                    currentWave++;
                    waveTimer = 0f;
                    ResetItemLists();

                }
            }

            timer += Time.deltaTime;

            //When timer reaches spawncountdown, call Spawn function
            if (timer >= totalSpawnCountdown)
            {
                Spawn();
            }
        }

        /// <summary>
        /// Goes through randomisation logic, and then calls <code>SpawnItem(Vector3 spawnLocation, int index)</code>
        /// </summary>
        public void Spawn()
        {

            if (useMaxSpawns == true && maxSpawnerSpawns <= 0) return;

            //Reset timer to 0 so process can start over
            timer = 0;

            //Select a random number, make sure it is a whole number with Absolute
            int randomSpawnPick = Random.Range(0, spawnChecks.Length);
            //int randomObjectPick = Random.Range(0, WaveScriptableObject.Length);

            spawnController = spawnChecks[randomSpawnPick]; //If we don't want to use only open spawn points, pick a random spawn point

            //Check whether there is already an item in the position we want to spawn
            //in, if it is clear, spawn a new item.
            if (spawnController.spawnEnabled == true)
            {
                if (useMaxPointSpawns == false || spawnController.maxSpawnsLeft > 0)
                {
                    spawnLocation = spawnPoints[randomSpawnPick];

                    //So, if we are using guaranteed wave spawning, we just spawn
                    //one object, then decrease its value in the scriptableobject, then return
                    if (useWaveSpawning)
                    {
                        for (int w = 0; w < spawnChances.Count; ++w)
                        {
                            if (spawnChances[w] > 0)
                            {
                                SpawnObject(spawnLocation.position, w);
                                //TODO send event, object has spawned
                                spawnChances[w]--;
                                if (OnSpawn != null)
                                    OnSpawn();

                                return;
                            }
                        }

                        return;
                    }

                    for (int n = 0; n < spawnChances.Count; ++n)
                    {
                        totalRatio += spawnChances[n];
                    }

                    //Assign a "random chance" integer to the item we are trying to spawn
                    int randomChance = Mathf.Abs(Random.Range(1, totalRatio));

                    for (int n = 0; n < spawnChances.Count + 1; ++n)
                    {
                        //Check the spawn ratio of the object we want to spawn
                        int objectIndex = CheckObjectIsBetween(n, randomChance);

                        //If we get the correct ratio, spawn the item
                        if (objectIndex != -1)
                        {
                            SpawnObject(spawnLocation.position, objectIndex);
                            //TODO send event, object has spawned
                            if (OnSpawn != null)
                                OnSpawn();
                            //Reset totalRatio
                            totalRatio = 0;
                            return;
                        }
                    }
                }
                else
                {
                    Debug.Log("Not spawning in location " + spawnController.name + ", it has no spawns left");
                }
            }


            //Reset totalRatio
            totalRatio = 0;

        }

        //This function handles the spawn ratios of the objects
        int CheckObjectIsBetween(int n, int middle)
        {
            int lower = 0;
            int upper = 0;

            //Loop through, checking the items spawn ratio
            for (int i = 0; i < n; ++i)
            {
                upper += spawnChances[i];

                if (lower < middle && middle < upper)
                {
                    //After checking the ratios, return the correct item to spawn
                    return i;
                }

                lower += spawnChances[i];
            }

            //Otherwise, return -1
            return -1;

        }

        /// <summary>
        /// Spawns the item.
        /// </summary>
        /// <param name="spawnLocation">Spawn location.</param>
        /// <param name="index">Index of item in object array.</param>
        protected virtual void SpawnObject(Vector3 spawnLocation, int index)
        {

            //Create the object at the point of the location variable after casting it
            //and make sure it is not null
            GameObject _gameObject = objectPrefabs[index] as GameObject;

            // Adds the wait time of the next object to the spawncountdown
            totalSpawnCountdown = spawnCountdown + spawnObject.waitBeforeSpawn[index];

            if (_gameObject != null)
            {

                GameObject newObject = Instantiate(_gameObject, spawnLocation, _gameObject.transform.rotation) as GameObject;


                //Output to debug
                Debug.Log("Spawning " + _gameObject.name.ToString());
            }

            if (_gameObject == null)
            {
                Debug.Log("The object you are trying to spawn has not been set.");
            }

            maxSpawnerSpawns--;//Decrease the max spawns this spawner can do
        }

        public void IncreaseSpawnerSpawns()
        {
            maxSpawnerSpawns++;
        }

        //Randomises a list
        public static List<T> Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            System.Random rnd = new System.Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            return randomizedList;
        }

    }

}