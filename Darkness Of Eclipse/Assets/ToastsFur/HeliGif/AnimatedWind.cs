using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWind : MonoBehaviour {

    public GifData gif;
    private MeshRenderer meshy;

	// Use this for initialization
	void Start () {
        meshy = GetComponent<MeshRenderer>();
        StartCoroutine(loop());
	}

    IEnumerator loop()
    {
        int i = 0;
        while (true)
        {
            meshy.material.SetTexture("_WindMap", gif.frames[i%gif.frames.Length]);
            i++;
            yield return new WaitForSeconds(gif.delay);
        }
    }
}
