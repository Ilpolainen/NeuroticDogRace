using UnityEngine;
using System.Collections;

public class HingeTest : MonoBehaviour {

	public float targetPos;
	public float thrust;
	private HingeJoint joint;
	// Use this for initialization
	void Start () {
		this.joint = this.gameObject.GetComponent<HingeJoint> ();
		this.joint.useMotor = true;
	}
	
	// Update is called once per frame
	void Update () {
		SteerWithMotor ();
	}

	public void SteerWithMotor() 
	{
		JointMotor motor = joint.motor;
		motor.targetVelocity = (targetPos - joint.angle) * thrust;
		motor.force = 40;
		joint.motor = motor;
		if (Time.frameCount % 40 == 0) {
			
			print ("Position is: " + joint.angle);
			print ("Target Velocity: " + (targetPos - joint.angle) * thrust);

			}

	}
}
