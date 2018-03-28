using UnityEngine;
using System.Collections;

namespace SAE.WaveManagerTool
{


    [System.Serializable]
    public class WaveScriptableObject : ScriptableObject
    {

        [SerializeField]
        public Object[] itemPrefab;

        [SerializeField]
        public int[] spawnChance;

        [SerializeField]
        public float[] waitBeforeSpawn;

        // TODO
        // Add field where possible spawn points are selected, all or multi selection

    }

}
