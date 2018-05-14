using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSensor : MonoBehaviour {

    LineRenderer lr;
    public float distance;
	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward * 10);
        Debug.DrawRay(transform.position, forward);
        if (Physics.Raycast(transform.position, forward, out hit)) {
            distance = hit.distance;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward * 10);
        Debug.DrawRay(transform.position, forward,Color.red);
        if (Physics.Raycast(transform.position,forward, out hit)) {
            distance = hit.distance;
        } else
        {
            distance = 50;
        }
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + forward * distance);
    }
}
