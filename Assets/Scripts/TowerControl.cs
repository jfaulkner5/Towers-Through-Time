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

    bool isActive = false;
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
    public bool isClosestEnemy;

    bool hasFired;
    public float towerFireCooldown;
    public float towerRange;
    bool isPaused;

    float towerBreakTimer;
    float towerBreakCurrent;
    float currentFreezeTime;

    public GameObject[] projectiles;

    #endregion

    void Start()
    {
        EventCore.Instance.eventFreeze.AddListener(PauseTimer);
        enemyList = new List<GameObject>();
        isActive = false;
        freezeTime = GameObject.FindGameObjectWithTag("Freeze").GetComponent<FreezeTime>().freezeTime;
        isPaused = false;
        timeToBreakCurrent = timeToBreak;

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

    public IEnumerator PauseTimer()
    {
        isPaused = true;
        yield return new WaitForSeconds(freezeTime);
        isPaused = false;
    }

    public IEnumerator BreakingTimer(float waitTime)
    {
        while (!isPaused)
        {
            yield return new WaitForSeconds(waitTime);
            PowerOff();
        }
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

        EnemyDistanceCheck();
        if (isClosestEnemy)
        {
            if (selectedEnemy != null)
            {
                hasFired = true;
                StartCoroutine(TowerFireDelay(towerFireCooldown));
                GameObject projectile = Instantiate(projectiles[GameManager.instance.visualTheme], transform);
                projectile.GetComponent<ProjectileMovement>().Initialize(selectedEnemy.transform);

                //Audio Call
                EventCore.Instance.towerFire.Invoke();
            }
        }
        else
        {
            EnemySelect();
        }
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

    private void EnemySelect()
    {
        var enemyTag = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemyTag)
        {
            if (closestEnemy == null && Vector3.Distance(this.transform.position, e.transform.position) <= towerRange)
            { //if the closest enemy hasn't been selected yet and this object is in tower range

                selectedEnemy = this.gameObject;
                continue;
            }

            if (Vector3.Distance(this.transform.position, e.transform.position) <= towerRange && Vector3.Distance(this.transform.position, e.transform.position) < Vector3.Distance(this.transform.position, closestEnemy.transform.position))
            { //if in range and closer than "closest"
                closestEnemy = e;
            }
        }

        selectedEnemy = closestEnemy;
        isClosestEnemy = true;
    }

    private void EnemyDistanceCheck()
    {
        if (Vector3.Distance(this.transform.position, selectedEnemy.transform.position) <= towerRange)
        {
            isClosestEnemy = true;
        }
        else
        {
            isClosestEnemy = false;
        }

    }


    //void EnemySelect()
    //{
    //    //should return a game object
    //    selectedEnemy = EnemyToAttack(gameObject);
    //}

    //public GameObject EnemyToAttack(GameObject towerCalling)
    //{
    //    //call function the create list of current enemies

    //    foreach (GameObject enemy in enemyList)
    //    {
    //        if (Vector3.Distance(towerCalling.transform.position, enemy.transform.position) <= towerRange)
    //        {
    //            closestEnemy = enemy;
    //        }
    //    }
    //    return closestEnemy;
    //}

    //void EnemyList()
    //{
    //    enemyList.Clear();
    //    objs = GameObject.FindGameObjectsWithTag("Enemy");

    //    foreach (GameObject Enemy in objs)
    //    {
    //        enemyList.Add(Enemy);
    //    }
    //}

}


