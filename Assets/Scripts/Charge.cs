using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charge : MonoBehaviour {

    public Image fillCircle;
    public TowerControl tower;
    bool isBreaking;

	// Use this for initialization
	void Start () {
        fillCircle.fillAmount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (tower.isActive)
        {
            if (!isBreaking)
            {
                fillCircle.color = Color.green;
                fillCircle.fillAmount = 1;
            }

        }
        else
        {
            fillCircle.fillAmount = tower.repairPercent;
            fillCircle.color = Color.red;
        }
	}
}
