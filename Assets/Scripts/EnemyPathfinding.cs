using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour {

    public Transform target;
    public NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Monument").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}
