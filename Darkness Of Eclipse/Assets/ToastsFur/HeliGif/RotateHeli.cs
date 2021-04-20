using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHeli : MonoBehaviour {

    private float rotY = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rotY += Time.deltaTime * 2000;
        transform.localRotation = Quaternion.Euler(0,rotY,0);
	}
}
