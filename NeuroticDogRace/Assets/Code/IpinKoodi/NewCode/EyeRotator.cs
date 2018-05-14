using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotator : MonoBehaviour {

    Vector3 startRot;
    
	// Use this for initialization
	void Start () {
        startRot = transform.localRotation.eulerAngles;
        startRot = new Vector3(startRot.x, startRot.y, startRot.z);
	}
	
	// Update is called once per frame
	

    public void RotateEyeHorizontal(float horizontal)
    { 
        Quaternion newrot = Quaternion.Euler(startRot.x, startRot.y + horizontal, startRot.z);
        transform.localRotation = newrot;
    }
    public void RotateEyeVertical(float vertical)
    {
        Quaternion newrot = Quaternion.Euler(startRot.x+vertical, startRot.y, startRot.z);
        transform.localRotation = newrot;
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
    }
}
