using UnityEngine;
using SAE.WaveManagerTool;

public class SpawnPointController : MonoBehaviour
{

    public bool spawnEnabled = true;

    //Used to determine if this spawn point has already spawned its maximum
    //allowed amount.
    public int maxSpawnsLeft;

    //public int spawnPointState = (int)SpawnPointStates.Enabled;

    public void EnableSpawnPoint()
    {
       // spawnPointState = (int)SpawnPointStates.Enabled;
        spawnEnabled = true;
        // Add too availble spawn list?
    }

    public void DisableSpawnPoint()
    {
       // spawnPointState = (int)SpawnPointStates.Disabled;
        spawnEnabled = false;
        // Remove from availble spawn list?
    }
}
