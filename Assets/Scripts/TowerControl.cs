using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerControl : MonoBehaviour
{



    //Possibly temp data
    // Use this for initialization
    #region

    [HideInInspector]
    public GameObject selectedEnemy;

    bool isActive = false;
    public float timeToRepair;
    public float timeToBreak;
    float currentRepairTime;

    //Enemy Selector

    private GameObject[] objs; //temp array for obj grabbing
    public List<GameObject> enemyList;
    public ParticleSystem powerOnParticle;
    public ParticleSystem powerOffParticle;


    GameObject closestEnemy;

    bool hasFired;
    public float towerFireCooldown;
    public float towerRange;

    public GameObject[] projectiles;

    #endregion

    void Start()
    {
        enemyList = new List<GameObject>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive != true)
            return;
        if (hasFired == true)
            return;
        TowerFire();
    }

    public IEnumerator BreakingTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PowerOff();
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
        StartCoroutine(BreakingTimer(timeToBreak));
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
        //Audio Call
        EventCore.Instance.towerFire.Invoke();

        EnemySelect();
        if (closestEnemy != null)
        {
            hasFired = true;
            StartCoroutine(TowerFireDelay(towerFireCooldown));
            GameObject projectile = Instantiate(projectiles[GameManager.instance.visualTheme],transform);
            projectile.GetComponent<ProjectileMovement>().Initialize(selectedEnemy.transform);
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
            if (currentRepairTime >= timeToRepair)
            {
                PowerOn();
            }
            isRepairing = true;
            
        }
    }

    public GameObject EnemyToAttack(GameObject towerCalling)
    {
        //call function the create list of current enemies
        EnemyList();

        foreach (GameObject enemy in enemyList)
        {
            if (Vector3.Distance(towerCalling.transform.position, enemy.transform.position) <= towerRange)
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    void EnemyList()
    {
        enemyList.Clear();
        objs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in objs)
        {
            enemyList.Add(Enemy);
        }
    }

}


