using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPawn : MonoBehaviour {

    public GameObject GO;
	// Use this for initialization
	void Start () {
        Invoke("Method", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void Method()
    {
        Instantiate(GO, transform);
    }
}
