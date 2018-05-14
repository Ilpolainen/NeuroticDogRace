using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    Transform cam;
    public float speed;
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

	void Start()
    {
        cam = transform.GetChild(0).transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            transform.RotateAround(transform.position, Vector3.up, -speed*5);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.RotateAround(transform.position, Vector3.up, speed*5);
        }
        if (Input.GetKey(KeyCode.I))
        {
            cam.RotateAround(cam.position, transform.right, speed * 5);
        }
        if (Input.GetKey(KeyCode.K))
        {
            cam.RotateAround(cam.position, transform.right, -speed * 5);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + speed * transform.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position - speed * transform.forward;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position - speed * transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + speed * transform.right;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = transform.position - speed/10 * transform.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + speed/10 * transform.up;
        }
        float x = Mathf.Min(xMax, Mathf.Max(xMin,transform.position.x));
        float z = Mathf.Min(zMax, Mathf.Max(zMin, transform.position.z));
        float y = transform.position.y;
        transform.position = new Vector3(x, y, z);
    }
}
