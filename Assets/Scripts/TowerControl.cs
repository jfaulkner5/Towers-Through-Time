using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{



    //Possibly temp data
    // Use this for initialization
    #region
    public List<GameObject> enemyList;
    public GameObject[] objs; //temp array to grab every enemy

    public GameObject selectedEnemy;

    bool isActive = false;
    public float timeToRepair;
    float currentRepairTime;



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
    }


    void EnemySelect()
    {
        //should return a game object
        selectedEnemy = GameManager.instance.EnemyToAttack(gameObject);
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

}


