using UnityEngine;
using System.Collections;
using System;

public abstract class MuscledBody : MonoBehaviour {

	public int id;

	private GameObject physGo;
	private Rigidbody rb;
	public GameObject target;
	private Renderer[] renderers;
	private HingeJoint[] joints;
	public Transform[] controlPoints;

	private Vector3[] initialPositions;
	private Quaternion[] initialRotations;

    public float power;
	public Touch[] touches;
	private float[] sensorInfo;

	public bool ready;

	void Start() 
	{
		if (target == null) 
		{
			target = this.gameObject;
		}
		physGo = gameObject;
		rb = physGo.GetComponentInChildren<Rigidbody> ();
		renderers = physGo.GetComponentsInChildren<Renderer> ();
		joints = physGo.GetComponentsInChildren<HingeJoint> ();
		SetSensors ();
		SetMotors ();
		ready = true;
	}



	void FixedUpdate()
	{
		if (ready) {
			UpdateSensors ();
		}
        if (Time.frameCount % 100 == 0)
        {
            //DebugSensorInfo();
        }
	}

	void UpdateSensors()
	{
        for (int i = 0; i < touches.Length; i++)
        {
            sensorInfo[i] = touches[i].touching;
        }
        for (int i = 0; i < joints.Length;i++)
        {
            sensorInfo[i + touches.Length] = joints[i].angle/(joints[i].limits.max-joints[i].limits.min);
        }
        
    }




	public void Move(float[] rawCommands, int divider, float speedthrust) 
	{
		for (int i = 0; i < joints.Length; i++) {
			joints [i].useLimits = true;
			JointLimits limits = joints [i].limits;
			float targetPos = (limits.max-limits.min) * rawCommands [i]/divider + (limits.max+limits.min)/2;
			JointMotor motor = joints [i].motor;
			motor.targetVelocity = (targetPos - joints [i].angle) * speedthrust;
			motor.force = power;
			joints [i].motor = motor;
		}
	}


	public void StoreInitialPositions() {
		Transform[] transforms = physGo.GetComponentsInChildren<Transform> ();
		initialPositions = new Vector3[transforms.Length];
		initialRotations = new Quaternion[transforms.Length];
		for(int i = 0; i < transforms.Length; i++){
			initialPositions [i] = transforms [i].position;
			initialRotations [i] = transforms [i].rotation;
		}
	}









	public void Reset() {
		HingeJoint[] hinges = physGo.transform.GetComponentsInChildren<HingeJoint> ();
		foreach (HingeJoint joint in hinges) {
			JointMotor motor = joint.motor;
			motor.targetVelocity = 0;
			motor.force = 0;
			joint.motor = motor;
		}
		Transform[] transforms = physGo.GetComponentsInChildren<Transform> ();
		Rigidbody[] rigidbodies = physGo.GetComponentsInChildren<Rigidbody> ();

		foreach (Rigidbody rb in rigidbodies) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		for (int i = 0; i < transforms.Length; i++) {
			transforms[i].position = initialPositions[i];
			transforms [i].rotation = initialRotations[i];
		}
	}



	//__________________________________________
	// ----------------------------------------
	// HELPER METHODS
	// ----------------------------------------
	//__________________________________________

	void SetMotors() 
	{
		foreach (HingeJoint joint in joints) {
			joint.useMotor = true;
			joint.useSpring = false;
			joint.useLimits = true;
		}
	}

	void SetSensors ()
	{
		SetTouches ();
		SetControlPoints ();
		SetSensorInfo ();
	}

	public virtual void SetControlPoints()
	{
	}

	public void SetTouches()
	{
		touches = physGo.transform.GetComponentsInChildren<Touch> ();
	}

	public virtual int xyz()
	{
		return 0;
	}

	public virtual void SetSensorInfo () 
	{
		sensorInfo = new float[touches.Length + joints.Length + xyz ()];
		Debug.Log (sensorInfo.Length);
	}


	//__________________________________________
	// ----------------------------------------
	// GETTERS AND SETTERS
	// ----------------------------------------
	//__________________________________________

	public void SetTarget(GameObject target) {
		this.target = target;
	}

	public HingeJoint[] GetJoints() 
	{
		return joints;
	}

	public float[] GetSensorInfo() {
		return(sensorInfo);
	}




	//__________________________________________
	// ----------------------------------------
	// DEBUGGERS
	// ----------------------------------------
	//__________________________________________

	public void DebugJoint(int k, float[] floats,int volume) {
		for (int i = 0; i < joints.Length; i++) {

			//Debug.Log ("Force " + floats [i]);
			float command = floats [i];
			JointSpring spring = joints [i].spring;
			spring.damper = 200;
			spring.spring = 500;
			if (i == k) {
				Debug.Log (joints [i].gameObject);
				spring.targetPosition = volume;
			} else {
				//motor.targetVelocity = command*4;
				spring.targetPosition = 0;
			}
			joints [i].spring = spring;
		}
	}

	public void DebugRaw(float[] raw) {
		for (int i = 0; i < raw.Length; i++) {
			print ("FROM STEERABLE STEER: " + raw [i]);
		}
	}

	public void DebugCommands(JointLimits limits, float command) {
		if (Time.frameCount % 30 == 0) {
			print ("FROM STEERABLE STEER");
			print ("LIMITS: min " + limits.min + ", max " + limits.max + " - COMMAND: " + command);
		}
	}

	public void PrintControlPoints() {
		for (int i = 0; i < controlPoints.Length; i++) {
			print ("Control point: " + i + ": " + controlPoints [i]);
		}
	}

    public void DebugSensorInfo()
    {
        Debug.Log("FROM MUSCLED BODY DEBUG SENSOR INFO");
        foreach (float c in sensorInfo)
        {
            print(c);
        }
    }
	// Use this for initialization

}
