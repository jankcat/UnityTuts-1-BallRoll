using UnityEngine;
using System.Collections;

public class PickUpColorChanger : MonoBehaviour {

    private int counter = 0;
    private int interval;
    private const int min = 15;
    private const int max = 120;

	void Start () {
        interval = Random.Range(min, max);
        var rend = GetComponent<Renderer>();
        var color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        rend.material.SetColor("_Color", color);
    }
	
	void Update () {
        counter++;
        if (counter < interval) return;
        counter = 0;
        interval = Random.Range(min, max);

        var rend = GetComponent<Renderer>();
        var color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        rend.material.SetColor("_Color", color);
    }
}
