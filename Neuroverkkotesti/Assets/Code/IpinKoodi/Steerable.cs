using UnityEngine;
using System.Collections;
using System;


public class Steerable : MonoBehaviour {

	public int id;

	public GameObject physGo;
	private Rigidbody rb;
	public Transform target;
	private Renderer[] renderers;
	private HingeJoint[] joints;
	public Transform[] controlPoints;

	private Vector3[] initialPositions;
	private Quaternion[] initialRotations;


	private float[] positionInfo;

	public Touch[] touches;



	void Start () {
		
		rb = physGo.GetComponent<Rigidbody> ();
		Initialize ();
		renderers = physGo.GetComponentsInChildren<Renderer> ();
		joints = physGo.GetComponentsInChildren<HingeJoint> ();
		touches = new Touch[4];

		SetControlPoints ();
		positionInfo = new float[controlPoints.Length * 3];
		setMotors ();
		UpDatePositionInfo ();

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
		UpDatePositionInfo ();
	}


	public void Steer(float[] floats) 
	{
		if (floats.Length != joints.Length) {
			Debug.LogError ("Wrong amount of commands!");
			return;
		}
		for (int i = 0; i < joints.Length; i++) {
			//Debug.Log ("Force " + floats [i]);
			float command = floats [i]*100;
			JointMotor motor = joints [i].motor;
			motor.targetVelocity = command;
			motor.force = 70;
			joints [i].motor = motor;
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
			positionInfo [3] = controlPoints [0].position.x - target.position.x;
			positionInfo [4] = controlPoints [0].position.z - target.position.z;
		}
		//UPDATES THE FOOT-TOUCHES
		for (int j = 0; j < 4; j++) {
			positionInfo [j + 5] = touches [j].touching;
		}
		int i = 9;
		for (int cp = 3; cp < controlPoints.Length; cp++) {
			positionInfo [i] = controlPoints[cp].position.x - controlPoints[0].position.x;
			i++;
			positionInfo [i] = controlPoints[cp].position.y - controlPoints[0].position.y;
			i++;
			positionInfo [i] = controlPoints[cp].position.z - controlPoints[0].position.z;
			i++;
		}
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
			//print (joint.gameObject);
		}
	}




	void SetControlPoints()
	{
		controlPoints = new Transform[8];
		Transform armature = physGo.transform.GetChild (0).transform;
		Transform pelvis = armature.GetChild (8).transform;
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

	private void Initialize() 
	{
		Transform[] transforms = physGo.GetComponentsInChildren<Transform> ();
		initialPositions = new Vector3[transforms.Length];
		initialRotations = new Quaternion[transforms.Length];
		for(int i = 0; i < transforms.Length; i++){
			initialPositions [i] = transforms [i].position;
			initialRotations [i] = transforms [i].rotation;
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

	public void PrintValueMeasures() {
		print ("Back: " + positionInfo[0] + ", Front: " + positionInfo[1]  + ", Head: " + positionInfo[2]);
	}
}
