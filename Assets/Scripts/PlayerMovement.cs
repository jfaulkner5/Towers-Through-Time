using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController cc;

    public float speed;
    Vector3 moveDir;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        
        moveDir = new Vector3(inputHorizontal * Time.deltaTime * speed, 0, inputVertical * Time.deltaTime * speed);
        cc.Move(moveDir);

        //if (moveDir != Vector3.zero)
        //{
        //    EventCore.Instance.playerWalk.Invoke();
        //}
    }
}