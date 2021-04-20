using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMapOffset : MonoBehaviour {

    public Vector2 speed;
    private MeshRenderer meshy;

	// Use this for initialization
	void Start () {
        meshy = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        meshy.material.SetTextureOffset("_WindMap", meshy.material.GetTextureOffset("_WindMap") + speed * Time.deltaTime);
	}
}
