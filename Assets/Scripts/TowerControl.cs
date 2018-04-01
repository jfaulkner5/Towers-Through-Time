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

    public GameObject selectedEnemy;

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


    void EnemySelect()
    {
        //should return a game object
        selectedEnemy = GameManager.instance.EnemyToAttack(this.gameObject);
    }
}


