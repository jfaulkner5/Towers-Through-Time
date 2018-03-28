//#define POOLMANAGER_INSTALLED

namespace SAE.WaveManagerTool
{

    using UnityEngine;

    /// <summary>
    /// 
    /// This script calls pulls an object from the pool manager or instanciates or destroys the object if no pool manager is installed.
    /// 
    /// Author: Garth de Wet <garthofhearts@gmail.com>
    /// Website: http://corruptedsmilestudio.blogspot.com/
    /// Date Modified: 09 Feb 2012
    /// 
    /// </summary>

    public static class ToolManager
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="prefab">The prefab to spawn an instance from</param>
        /// <param name="pos">The position to spawn the instance</param>
        /// <param name="rot">The rotation of the new instance</param>
        /// <returns></returns>
        public static Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
        {
#if POOLMANAGER_INSTALLED
            // If the pool doesn't exist, it will be created before use
            if (!PoolManager.Pools.ContainsKey(poolName))
                (new GameObject(poolName)).AddComponent<SpawnPool>();

            return PoolManager.Pools[poolName].Spawn(prefab, pos, rot);
#else
            // Use SpawnItem() in WaveManager
            return (Transform)Object.Instantiate(prefab, pos, rot);
#endif
        }

        /// <summary>
        /// Despawns an instance.
        /// </summary>
        public static void Despawn(Transform instance)
        {
#if POOLMANAGER_INSTALLED
            PoolManager.Pools[poolName].Despawn(instance);
#else

            // Redudent without pool manager
            Object.Destroy(instance.gameObject);
#endif
        }

        /// <summary>
        /// This is used by Spawner.cs, You should never need to access this.
        /// This is only used when Pool Manager is present.
        /// </summary>
        /// <param name="prefab">The Prefab to start pooling.</param>
        /// <param name="amount">The number of prefabs to pool.</param>
        public static void ReadyPreSpawn(Transform prefab, int amount)
        {
#if POOLMANAGER_INSTALLED
            if (!PoolManager.Pools.ContainsKey(poolName))
                (new GameObject(poolName)).AddComponent<SpawnPool>();

            PrefabPool item = new PrefabPool(prefab);
            item.preloadAmount = amount;

            PoolManager.Pools[poolName].CreatePrefabPool(item);

#endif
        }

    }
}