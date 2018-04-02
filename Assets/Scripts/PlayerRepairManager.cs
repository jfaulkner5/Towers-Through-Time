using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepairManager : MonoBehaviour {
    GameObject[] Towers;

[Tooltip("The distance the player must be to start repairing a tower")]
    public float distToRepair;

	// Use this for initialization
	void Start () {
        Towers = GameObject.FindGameObjectsWithTag("Tower");

        

	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject tower in Towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance <= distToRepair)
            {
                //TODO make tower repair happen
            }
        }
	}
}
