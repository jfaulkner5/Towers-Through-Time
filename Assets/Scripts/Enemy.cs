using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform monument;

    public float enemyHeight;

    public int enemyDamage;
    Vector3 monumentPos;
    float distToMonument;
    [Tooltip("How far away the enemy must be before they hit the tower")]
    public float distToMonumentToDie;

    private void Start()
    {
        monument = Monument.instance.transform;
        monumentPos = new Vector3(monument.transform.position.x, monument.transform.position.y + enemyHeight, monument.transform.position.z);
    }

    private void Update()
    {
        distToMonument = Vector3.Distance(transform.position, monumentPos);
        if (distToMonument <= distToMonumentToDie)
        {
            Monument.instance.TakeDamage(enemyDamage);
            Die();
        }

    }

    public void Die()
    {
        //DO ALL DEATH EFFECTS, ETC HERE
        Destroy(gameObject);
    }
}
