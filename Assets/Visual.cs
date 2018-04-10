using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visual : MonoBehaviour {

    public Image image;
    Camera cam;

	// Use this for initialization
	void Start () {
        image.transform.position = PlacePosition(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3 PlacePosition(Vector3 worldPoint)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        cam = Camera.main;
        Vector3 screenPointUnscaled = cam.WorldToScreenPoint(worldPoint);
        screenPointUnscaled /= canvas.scaleFactor;
        return worldPoint;
    }
}
