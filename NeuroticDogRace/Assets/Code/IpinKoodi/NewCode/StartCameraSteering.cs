using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraSteering : MonoBehaviour {

    public GameObject go;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.J))
        {
            transform.RotateAround(go.transform.position,Vector3.up,speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.RotateAround(go.transform.position, Vector3.up, -speed);
        }
        if (Input.GetKey(KeyCode.I))
        {
            transform.RotateAround(go.transform.position, transform.right, speed);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.RotateAround(go.transform.position, transform.right, -speed);
        }
    }
}
