using UnityEngine;
using System.Collections;

public class PickUpRotator : MonoBehaviour {
	
	void Update () {
        transform.Rotate(new Vector3(Phase(30 * direction), Phase(30 * direction), Phase(30 * direction)) * Time.deltaTime);
	}

    private int direction;

    void Start()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                direction = 1;
                break;
            case 2:
                direction = -1;
                break;
            default:
                direction = 1;
                break;
        }
    }

    private int Phase(int input)
    {
        return input + Random.Range(-20, 20);
    }
}
