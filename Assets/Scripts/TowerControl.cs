using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject closestEnemy;
    public Enemy _enemyScript;

    #endregion

    void Start()
    {
        enemyList = new List<GameObject>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

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
        //Audio Call
        EventCore.Instance.towerFire.Invoke();

        EnemySelect();

        EventCore.Instance.enemyToKill.Invoke(selectedEnemy);


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
            if (closestEnemy == null)
            {
                closestEnemy = this.gameObject;
            }
            else if (Vector3.Distance(towerCalling.transform.position, enemy.transform.position) <= Vector3.Distance(closestEnemy.transform.position, towerCalling.transform.position))
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    void EnemyList()
    {
        objs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in objs)
        {
            enemyList.Add(Enemy);
        }
    }

}


