using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{



    //Possibly temp data
    // Use this for initialization
    #region
    public List<GameObject> enemyList;
    public GameObject[] objs; //temp array to grab every enemy
    #endregion

    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator WaitTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    void PowerOn()
    {
        //Audio Call event
        EventCore.Instance.towerOn.Invoke();
    }

    void PowerOff()
    {
        //Audio Call
        EventCore.Instance.towerOff.Invoke();
    }

    public void TowerFire()
    {
        //Audio Call
        EventCore.Instance.towerFire.Invoke();
    }

    //[HACK] should only be called every time they fire 
    //[TODO] create seperate manager to pass to towers
    void EnemySelect()
    {
        objs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in objs)
        {
            enemyList.Add(Enemy);
        }
    }
}


