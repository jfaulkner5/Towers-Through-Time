using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour {

    public Transform target;
    public NavMeshAgent agent;

    public float speed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Monument").transform;
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            transform.position = closestHit.position;
            agent = gameObject.AddComponent<NavMeshAgent>();
            //TODO
        }
        agent.SetDestination(target.position);
    }
}
