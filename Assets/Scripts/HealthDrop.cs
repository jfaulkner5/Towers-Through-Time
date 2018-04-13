using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("picked up");
            EventCore.Instance.healthPickup.Invoke();
            Destroy(gameObject);
        }
    }
}
