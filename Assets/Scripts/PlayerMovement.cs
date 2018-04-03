using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
	
	// Update is called once per frame
	void Update () {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(inputHorizontal, 0, inputVertical);

        transform.position += moveDir * Time.deltaTime * speed;

        if(inputHorizontal != 0 || inputVertical != 0)
        {
            EventCore.Instance.playerWalk.Invoke();
        }
    }
}