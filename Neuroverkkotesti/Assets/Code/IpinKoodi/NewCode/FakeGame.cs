using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGame : MonoBehaviour {

    GameObject currentDog;
    public GameObject dog;
	// Use this for initialization
	void Start () {
        currentDog = GameObject.Instantiate(dog);
	}
	
	// Update is called once per frame
	void Update () {
		if (ItsTime())
        {
            GameObject.Destroy(currentDog);
            currentDog = GameObject.Instantiate(dog);
        }
	}

    private bool ItsTime()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
}
