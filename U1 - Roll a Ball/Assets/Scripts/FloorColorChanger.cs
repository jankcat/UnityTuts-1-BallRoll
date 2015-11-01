using UnityEngine;
using System.Collections;

public class FloorColorChanger : MonoBehaviour {

    private bool directionUp = true;
    private float blue = 0.235f;
    private const float step = 0.001f;

    void Update () {
        if (blue >= 0.675f) directionUp = false;
        if (blue <= 0.235f) directionUp = true;
        if (directionUp) blue += step;
        else blue -= step;

        var rend = GetComponent<Renderer>();
        var color = new Color(0.0078f, 0.1961f, blue);
        rend.material.SetColor("_Color", color);
    }
}
