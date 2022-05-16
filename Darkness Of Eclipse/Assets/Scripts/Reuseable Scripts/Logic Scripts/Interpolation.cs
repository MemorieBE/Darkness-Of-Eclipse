using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interpolation
{
    public static float LinearToParabolic(float x, float ease)
    {
        if (ease == 0) { return x; }
        else { return 0.5f / ease * Mathf.Pow(x - 1f, 2f) + ease / 2f; }
    }
}
