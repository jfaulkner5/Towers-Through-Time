using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerControl : MonoBehaviour
{

    //Possibly temp data
    // Use this for initialization
    #region variables

    [HideInInspector]
    public GameObject selectedEnemy;


    public float timeToRepair;
    public float timeToBreak;
    float timeToBreakCurrent;
    float currentRepairTime;
    float freezeTime;

    //Enemy Selector

    private GameObject[] objs; //temp array for obj grabbing
    public List<GameObject> enemyList;
    public ParticleSystem powerOnParticle;
    public ParticleSystem powerOffParticle;


    GameObject closestEnemy;

    bool hasFired;
    public float towerFireCooldown;
    public float towerRange;
    bool isPaused;
    bool isActive = false;
    float towerBreakTimer;
    float towerBreakCurrent;
    float currentFreezeTime;

    public GameObject[] projectiles;

    #endregion

    void Start()
    {
        EventCore.Instance.enemySpawned.AddListener(OnEnemySpawned);
        EventCore.Instance.eventFreeze.AddListener(PauseTimer);
        enemyList = new List<GameObject>();
        isActive = false;
        //[fix] this is currently throwing an error
        if (GameObject.FindGameObjectWithTag("Freeze") != null)
            freezeTime = GameObject.FindGameObjectWithTag("Freeze").GetComponent<FreezeTime>().freezeTime;
        isPaused = false;
        timeToBreakCurrent = timeToBreak;
    }

    void OnEnemySpawned(EventCore.EnemySpawnedData data)
    {
        enemyList.Add(data.enemySpawned);
    }


    void PauseTimer(EventCore.FreezeData data)
    {
        if (isActive)
            timeToBreakCurrent += freezeTime;
    }

    // Update is called once per frame
    void Update()
    {
        BreakingTimer();
        if (isActive != true)
            return;
        if (hasFired == true)
            return;
        TowerFire();
    }

    void BreakingTimer()
    {
        if (towerBreakCurrent != 0)
        {
            towerBreakCurrent += Time.deltaTime;
            if (towerBreakCurrent >= timeToBreakCurrent)
            {
                PowerOff();
                towerBreakCurrent = 0;
                timeToBreakCurrent = timeToBreak;
            }
        }
    }

    public IEnumerator TowerFireDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hasFired = false;
    }

    public void PowerOn()
    {
        //Audio Call event
        powerOnParticle.Play();
        EventCore.Instance.towerOn.Invoke();
        print("POWER ON FOR " + gameObject.name);
        isActive = true;
        currentRepairTime = 0;
        towerBreakCurrent += Time.deltaTime;
    }

    public void PowerOff()
    {
        powerOffParticle.Play();
        print("POWER OFF FOR " + gameObject.name);
        //Audio Call
        EventCore.Instance.towerOff.Invoke();
        isActive = false;
    }

    public void TowerFire()
    {
        EnemySelect();
        if (closestEnemy != null)
        {
            hasFired = true;
            StartCoroutine(TowerFireDelay(towerFireCooldown));
            GameObject projectile = Instantiate(projectiles[GameManager.instance.visualTheme],transform);
            projectile.GetComponent<ProjectileMovement>().Initialize(selectedEnemy.transform);

            //Audio Call
            EventCore.Instance.towerFire.Invoke();
        }
        EventCore.Instance.towerFireStop.Invoke();
    }


    void EnemySelect()
    {
        //should return a game object
        selectedEnemy = EnemyToAttack(gameObject);
    }

    public void Repair(out bool isRepairing)
    {
        if (isActive)
        {
            isRepairing = false;
            return;
        }
        else
        {
            print("REPAIRING " + gameObject.name);
            currentRepairTime += Time.deltaTime;
            isRepairing = true;
            if (currentRepairTime >= timeToRepair)
            {
                PowerOn();
                isRepairing = false;
            }          
        }
    }

    public GameObject EnemyToAttack(GameObject towerCalling)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(enemyList[i]);
                continue;
            }
            if (Vector3.Distance(towerCalling.transform.position, enemyList[i].transform.position) <= towerRange)
            {
                closestEnemy = enemyList[i];
            }
        }
        return closestEnemy;
    }
}