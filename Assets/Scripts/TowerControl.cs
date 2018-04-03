using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerControl : MonoBehaviour
{



    //Possibly temp data
    // Use this for initialization
    #region

    public GameObject selectedEnemy;

    bool isActive = false;
    public float timeToRepair;
    float currentRepairTime;

    //Enemy Selector

    private GameObject[] objs; //temp array for obj grabbing
    public List<GameObject> enemyList;

    GameObject closestEnemy;

    bool hasFired;
    public float towerFireCooldown;
    public float towerRange;

    #endregion

    void Start()
    {
        enemyList = new List<GameObject>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFired == true)
            return;
        TowerFire();
    }

    public IEnumerator WaitTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public void PowerOn()
    {
        //Audio Call event
        EventCore.Instance.towerOn.Invoke();
        print("POWER ON FOR " + gameObject.name);
        isActive = true;

    }

    public void PowerOff()
    {
        //Audio Call
        EventCore.Instance.towerOff.Invoke();
        isActive = false;
    }

    public void TowerFire()
    {
        print("fire");
        //Audio Call
        EventCore.Instance.towerFire.Invoke();

        EnemySelect();
        if (closestEnemy != null)
        {
            EventCore.Instance.enemyToKill.Invoke(selectedEnemy);
            hasFired = true;
            WaitTimer(towerFireCooldown);
            hasFired = false;
        }
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


