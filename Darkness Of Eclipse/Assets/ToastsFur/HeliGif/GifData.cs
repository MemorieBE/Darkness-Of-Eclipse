using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifData : ScriptableObject {

    public float endurance;
    public float delay;
    public Texture2D[] frames;
    
    public void Remove()
    {
        foreach (Texture2D t in frames)
        {
            Destroy(t);
        }
        System.GC.Collect();
    }
}
