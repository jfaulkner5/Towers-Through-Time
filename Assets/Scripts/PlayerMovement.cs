using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
	
	// Update is called once per frame
	void Update () {
       // float inputHorizontal = Input.GetAxis("Horizontal");
       // float inputVertical = Input.GetAxis("Vertical");

        //Vector3 moveDir = new Vector3(inputHorizontal, 0, inputVertical);

        //transform.position += moveDir * Time.deltaTime * speed;

        //if(inputHorizontal != 0 || inputVertical != 0)
        //{
        //    EventCore.Instance.playerWalk.Invoke();
       // }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}