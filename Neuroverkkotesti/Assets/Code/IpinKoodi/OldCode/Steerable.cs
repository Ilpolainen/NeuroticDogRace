using UnityEngine;
using System.Collections;
using System;


public class Steerable : MonoBehaviour {

	public int id;

	public GameObject physGo;
	private Rigidbody rb;
	public GameObject target;
	private Renderer[] renderers;
	private HingeJoint[] joints;
	public Transform[] controlPoints;

	private Vector3[] initialPositions;
	private Quaternion[] initialRotations;


	private float[] positionInfo;
	public Touch[] touches;

	public bool ready;

	public void Initialize() 
	{
		rb = physGo.GetComponent<Rigidbody> ();
		Transform[] transforms = physGo.GetComponentsInChildren<Transform> ();
		initialPositions = new Vector3[transforms.Length];
		initialRotations = new Quaternion[transforms.Length];
		for(int i = 0; i < transforms.Length; i++){
			initialPositions [i] = transforms [i].position;
			initialRotations [i] = transforms [i].rotation;
		}
		renderers = physGo.GetComponentsInChildren<Renderer> ();
		joints = physGo.GetComponentsInChildren<HingeJoint> ();
		touches = new Touch[4];

		SetControlPoints ();
		positionInfo = new float[controlPoints.Length * 3 + 3];
		setMotors ();
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown("space")) {
			hide ();
		}
		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			show ();
		}
	}

	void FixedUpdate()
	{
		if (ready) {
			UpDatePositionInfo ();
		}
	}


	public void SteerWithMotor(float[] raw, int divider, float speedthrust) 
	{
		for (int i = 0; i < joints.Length; i++) {
			joints [i].useLimits = true;
			JointLimits limits = joints [i].limits;
			float targetPos = (limits.max-limits.min) * raw [i]/divider + (limits.max+limits.min)/2;
			JointMotor motor = joints [i].motor;
			motor.targetVelocity = (targetPos - joints [i].angle) * speedthrust;

			motor.force = 40;
			joints [i].motor = motor;
			if (id == 1 && i == 7) {
				print ("FROM STEERABLE STEERWITHMOTOR: JOINT " + joints [i].gameObject +  ", Targetposition is: " + targetPos);
				print ("Current angle is: " + joints [i].angle);
				print ("Target Velocity: " + (targetPos - joints [i].angle) * speedthrust);
				print ("Max: " + limits.max + ", Min: " + limits.min);
			}
		}
	}

	public void SteerWithSpring(float[] raw, int divider) 
	{
		for (int i = 0; i < joints.Length; i++) {
			JointLimits limits = joints [i].limits;
			float targetPos = (limits.max-limits.min) * raw [i]/divider + (limits.max+limits.min)/2;
			JointSpring spring = joints [i].spring;
			spring.targetPosition = targetPos;
			spring.damper = 100;
			spring.spring = 400;
			joints [i].spring = spring;
			if (id == 1 && i == 6 && Time.frameCount % 40 == 5) {
				print ("FROM STEERABLE STEER: JOINT " + joints [i].gameObject +  ", Targetposition is: " + joints[i].spring.targetPosition);
				print ("Max: " + limits.max + ", Min: " + limits.min);
				print ("Position is: " + joints [i].angle);
			}
		}
	}

	void DebugRaw(float[] raw) {
		for (int i = 0; i < raw.Length; i++) {
			print ("FROM STEERABLE STEER: " + raw [i]);
		}
	}

	void DebugCommands(JointLimits limits, float command) {
		if (Time.frameCount % 30 == 0) {
			print ("FROM STEERABLE STEER");
			print ("LIMITS: min " + limits.min + ", max " + limits.max + " - COMMAND: " + command);
		}
	}

	void UpDatePositionInfo() 
	{	
		positionInfo [0] = controlPoints [0].position.y;
		positionInfo [1] = controlPoints [1].position.y;
		positionInfo [2] = controlPoints [2].position.y;
		if (target == null) {
			positionInfo [3] = 0;
			positionInfo [4] = 0;
		} else {
			positionInfo [3] = controlPoints [8].localPosition.x - target.transform.localPosition.x;
			positionInfo [4] = controlPoints [8].localPosition.z - target.transform.localPosition.z;
		}
		//UPDATES THE FOOT-TOUCHES
		for (int j = 0; j < 4; j++) {
			positionInfo [j + 5] = touches [j].touching;
		}
		int i = 9;
		for (int cp = 3; cp < controlPoints.Length-1; cp++) {
			positionInfo [i] = controlPoints[cp].position.x - controlPoints[0].position.x;
			i++;
			positionInfo [i] = controlPoints[cp].position.y - controlPoints[0].position.y;
			i++;
			positionInfo [i] = controlPoints[cp].position.z - controlPoints[0].position.z;
			i++;
		}
		positionInfo [positionInfo.Length - 3] = controlPoints [8].rotation.eulerAngles.x;
		positionInfo [positionInfo.Length - 2] = controlPoints [8].rotation.eulerAngles.y;
		positionInfo [positionInfo.Length - 1] = controlPoints [8].rotation.eulerAngles.z -90;
	}

	public float[] GetPositionInfo() {
		return(positionInfo);
	}


	void hide() 
	{
		ComponentUtilities.HideObject (this.renderers);
	}


	void show() 
	{
		ComponentUtilities.ShowObject (this.renderers);
	}

	void setMotors() 
	{
		foreach (HingeJoint joint in joints) {
			joint.useMotor = true;
			joint.useSpring = false;
			joint.useLimits = true;

			//print (joint.gameObject);
		}
		//print ("joints" + joints.Length);
	}




	void SetControlPoints()
	{
		controlPoints = new Transform[9];
		Transform armature = physGo.transform.GetChild (0).transform;
		Transform pelvis = armature.GetChild (8).transform;
		controlPoints [8] = armature;
		Transform measurePointBack = physGo.transform.GetChild (2).transform;
		Transform measurePointFront = physGo.transform.GetChild (3).transform;
		Transform measurePointHead = physGo.transform.GetChild (4).transform;

		controlPoints [0] = measurePointBack;
		controlPoints [1] = measurePointFront;
		controlPoints [2] = measurePointHead;


		Transform neck = pelvis.GetChild (0).transform.GetChild (0).transform;
		controlPoints [3] = neck;
		//UPPERLEGS
		Transform head = neck.GetChild(0).transform;

		Transform BLU = armature.GetChild (2).transform;
		//controlPoints [2] = BLU;
		Transform BRU = armature.GetChild (3).transform;
		//controlPoints [3] = BRU;
		Transform FLU = armature.GetChild (6).transform;
		//controlPoints [6] = FLU;
		Transform FRU = armature.GetChild (7).transform;
		//controlPoints [7] = FRU;

		//LOWERLEGS

		Transform BLD = BLU.GetChild(0).transform;
		touches [0] = BLD.GetComponent<Touch> ();

		//controlPoints [4] = BLD;
		Transform BRD = BRU.GetChild(0).transform;
		touches [1] = BRD.GetComponent<Touch> ();
		//controlPoints [5] = BRD;
		Transform FLD = FLU.GetChild(0).transform;
		touches [2] = FLD.GetComponent<Touch> ();
		//controlPoints [6] = FLD;
		Transform FRD = FRU.GetChild(0).transform;
		touches [3] = FRD.GetComponent<Touch> ();
		//controlPoints [7] = FRD;

		//FEET

		Transform BLFoot = BLD.GetChild (0).transform;
		controlPoints [4] = BLFoot;

		Transform BRFoot = BRD.GetChild (0).transform;
		controlPoints [5] = BRFoot;

		Transform FLFoot = FLD.GetChild (0).transform;
		controlPoints [6] = FLFoot;

		Transform FRFoot = FRD.GetChild (0).transform;
		controlPoints [7] = FRFoot;

	}

	public HingeJoint[] GetJoints() 
	{
		return joints;
	}

	//DEBUGGER-UTILITIES

	private void printControlPoints() {
		for (int i = 0; i < controlPoints.Length; i++) {
			print ("Control point: " + i + ": " + controlPoints [i]);
		}
	}


	public void Reset() {
		HingeJoint[] hinges = physGo.GetComponentsInChildren<HingeJoint> ();
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

	public void SetTarget(GameObject target) {
		this.target = target;
	}

	public void PrintValueMeasures() {
		print ("Back: " + positionInfo[0] + ", Front: " + positionInfo[1]  + ", Head: " + positionInfo[2] + "Rotation: " + (controlPoints[0].rotation.eulerAngles.x - 90));
	}

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
}
