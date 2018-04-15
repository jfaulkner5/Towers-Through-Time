using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform monument;

    public float enemyHeight;

    public GameObject death_PS;
    public int enemyDamage;
    Vector3 monumentPos;
    float distToMonument;
    [Tooltip("How far away the enemy must be before they hit the tower")]
    public float distToMonumentToDie;
    //TODO change
    [Tooltip("On death, a number between this and 0 will be chosen to determine whether to drop a health pack. The higher the number, the less of a chance")]
    public float randomChanceToDrop;
    public GameObject healthDrop;

    private void Start()
    {
        monument = Monument.instance.transform;
        
        monumentPos = new Vector3(monument.transform.position.x, monument.transform.position.y + enemyHeight, monument.transform.position.z);
    }

    private void Update()
    {
        distToMonument = Vector3.Distance(transform.position, monumentPos);
        //Debug.Log("distance to monument " + distToMonument, this.gameObject);
        if (distToMonument <= distToMonumentToDie)
        {
            Monument.instance.TakeDamage(enemyDamage);
            Die();
            Debug.Log("die function should be called");
        }
    }

    public void Die()
    {
        var data = new EventCore.EnemyDiedData();
        data.deadEnemy = gameObject;
        EventCore.Instance.enemyDied.Invoke(data);

        float rand = Random.Range(0, randomChanceToDrop);
        if (rand <= 1)
        {
            GameObject drop = Instantiate(healthDrop, transform.position, Quaternion.identity, null);
        }
        //DO ALL DEATH EFFECTS, ETC HERE
        GameObject death = Instantiate(death_PS, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }


    
}
