using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FW : MonoBehaviour {

    HingeJoint joint;
    JointMotor motor;
	// Use this for initialization
	void Start () {
        joint = gameObject.GetComponent<HingeJoint>();
        JointMotor motor = joint.motor;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            motor.force = 30;
            motor.targetVelocity = -1900;
            joint.motor = motor;
            joint.useMotor = true;
        } else {
            joint.useMotor = false;
        }
	}

}
