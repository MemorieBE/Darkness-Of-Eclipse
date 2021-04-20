using UnityEngine;
using System.Collections;

public class GodCam : MonoBehaviour {

    public float moveFactor = 50;
    public float rotationFactor = 100;
    private float rotX;
    private float rotY;

    // Use this for initialization
    void Start () {
        rotX = transform.rotation.eulerAngles.x;
        rotY = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.TransformDirection(Input.GetAxis("Horizontal") * Vector3.right * moveFactor * Time.deltaTime);
        transform.position += transform.TransformDirection(Input.GetAxis("Vertical") * Vector3.forward * moveFactor * Time.deltaTime);
        rotY += Input.GetAxis("Mouse X") * rotationFactor * Time.deltaTime;
        rotX -= Input.GetAxis("Mouse Y") * rotationFactor * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotX,rotY,0);
    }
}
