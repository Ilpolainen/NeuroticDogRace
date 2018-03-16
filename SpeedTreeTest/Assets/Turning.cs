using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {

    HingeJoint joint;
    JointMotor motor;
	// Use this for initialization
	void Start () {
        joint = gameObject.GetComponent<HingeJoint>();
        motor = joint.motor;
        JointLimits limits = joint.limits;
        int lim = 30;
        limits.min = -lim;
        limits.max = lim;
        joint.limits = limits;
        joint.useLimits = true;
	}
	
	// Update is called once per frame
	void Update () {
        int targetVel = 400;
        float motorFor = 40;
		if (Input.GetKey(KeyCode.LeftArrow))
        {
            motor.force = motorFor;
            motor.targetVelocity = -targetVel;
            joint.motor = motor;
            joint.useMotor = true;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            motor.force = motorFor;
            motor.targetVelocity = targetVel;
            joint.motor = motor;
            joint.useMotor = true;
        } else
        {
            motor.force = motorFor;
            motor.targetVelocity = 0;
            joint.motor = motor;
            joint.useMotor = true;
        }
	}
}
