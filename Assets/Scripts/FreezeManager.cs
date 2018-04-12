using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour {

    bool canFreezeTime = true;
    float freezeTimeCooldown;
    public float freezeTime;
    float enemySpeed;
    public float timeBetweenFreezes;


    public static FreezeManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
        canFreezeTime = true;
    }

    private void Update()
    {
        if (!canFreezeTime)
        {
            freezeTimeCooldown += Time.deltaTime;
            if (freezeTimeCooldown >= timeBetweenFreezes)
            {
                canFreezeTime = true;
                freezeTimeCooldown = 0;
            }
        }
    }

    public void CheckForFreeze(out bool doFreeze)
    {
        if (canFreezeTime)
        {
            StartCoroutine(Freeze());
            StopCoroutine(Freeze());
            doFreeze = true;
        }
        else
        {
            doFreeze = false;
        }
    }

    IEnumerator Freeze()
    {
        canFreezeTime = false;
        EventCore.FreezeData data = new EventCore.FreezeData();
        EventCore.Instance.eventFreeze.Invoke(data);
        GameObject[] thing = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in thing)
        {
            if (enemy != null)
            {
                enemySpeed = enemy.GetComponent<EnemyPathfinding>().speed;
                enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
            }
        }
        yield return new WaitForSeconds(freezeTime);
        foreach (GameObject enemy in thing)
        {
            if (enemy != null)
                enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = enemySpeed;
        }
    }
}
