using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

    [HideInInspector]
    public Transform target;
    public float speed;


    public void Initialize(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if (target == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().Die();
            
            Destroy(gameObject);
        }
    }
}
