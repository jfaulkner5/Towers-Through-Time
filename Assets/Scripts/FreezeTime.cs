using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAE.WaveManagerTool;
using UnityEngine.AI;

public class FreezeTime : MonoBehaviour {

    public ParticleSystem freezeTime_PS;
    public float timeBetweenFreezes;
    public float freezeTime;
    float enemySpeed;
    bool canFreezeTime;
    float freezeTimeCooldown;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canFreezeTime)
        {
            freezeTime_PS.Play();
            StartCoroutine(Freeze());
        }
    }

    IEnumerator Freeze()
    {
        EventCore.FreezeData data = new EventCore.FreezeData();
        EventCore.Instance.eventFreeze.Invoke(data);
        GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>().timer -= freezeTime;
        GameObject[] thing = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in thing)
        {
            if (enemy != null)
            {
                enemySpeed = enemy.GetComponent<NavMeshAgent>().speed;
                enemy.GetComponent<NavMeshAgent>().speed = 0;
            }
        }
        yield return new WaitForSeconds(freezeTime);
        foreach (GameObject enemy in thing)
        {
            if (enemy != null)
                enemy.GetComponent<NavMeshAgent>().speed = enemySpeed;
        }
    }

}
