using UnityEngine;
using System.Collections;
using System;


public class Steerable : MonoBehaviour {

	public GameObject physGo;
	private Rigidbody rb;
	private Renderer[] renderers;
	private HingeJoint[] joints;
	public Transform[] controlPoints;
	private float[] positionInfo;


	// Use this for initialization

	void Start () {
		
		rb = physGo.GetComponent<Rigidbody> ();
		renderers = physGo.GetComponentsInChildren<Renderer> ();
		joints = physGo.GetComponentsInChildren<HingeJoint> ();

		SetControlPoints ();
		positionInfo = new float[controlPoints.Length * 3];
		setMotors ();
		UpDatePositionInfo ();

	}
	
	// Update is called once per frame
	void Update () {
		UpDatePositionInfo ();
		if (Input.GetKeyDown("space")) {
			hide ();
		}
		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			show ();
		}
	}

	void FixedUpdate()
	{
		
	}


	public void Steer(float[] floats) 
	{
		if (floats.Length != joints.Length) {
			Debug.LogError ("Wrong amount of commands!");
			return;
		}
		for (int i = 0; i < joints.Length; i++) {
			//Debug.Log ("Force " + floats [i]);
			float command = floats [i]*400;
			JointMotor motor = joints [i].motor;
			motor.targetVelocity = command;
			motor.force = 50;
			joints [i].motor = motor;
		}
	}


	void UpDatePositionInfo() 
	{	
		positionInfo [0] = controlPoints [0].position.x;
		positionInfo [1] = controlPoints [0].position.y;
		positionInfo [2] = controlPoints [0].position.z;
		int i = 3;
		for (int cp = 1; cp < controlPoints.Length; cp++) {
			positionInfo [i] = controlPoints[cp].position.x - controlPoints[0].position.x;
			i++;
			positionInfo [i] = controlPoints[cp].position.y - controlPoints[0].position.y;
			i++;
			positionInfo [i] = controlPoints[cp].position.z - controlPoints[0].position.z;
			i++;
			//print ("Controlpoint " + controlPoints [cp].gameObject + ": (" + positionInfo [i - 3] + "," + positionInfo [i - 2] + "," + positionInfo [i - 1] + ")");
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
		controlPoints = new Transform[10];

		Transform centrePoint = physGo.transform;
		controlPoints [0] = centrePoint;

		Transform armature = physGo.transform.GetChild (0).transform;
		Transform pelvis = armature.GetChild (8).transform;

		Transform neck = pelvis.GetChild (0).transform.GetChild (0).transform;
		controlPoints [1] = neck;

		//UPPERLEGS

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
		controlPoints [2] = BLD;

		Transform BRD = BRU.GetChild(0).transform;
		controlPoints [3] = BRD;

		Transform FLD = FLU.GetChild(0).transform;
		controlPoints [4] = FLD;

		Transform FRD = FRU.GetChild(0).transform;
		controlPoints [5] = FRD;

		//FEET

		Transform BLFoot = BLD.GetChild (0).transform;
		controlPoints [6] = BLFoot;

		Transform BRFoot = BRD.GetChild (0).transform;
		controlPoints [7] = BRFoot;

		Transform FLFoot = FLD.GetChild (0).transform;
		controlPoints [8] = FLFoot;

		Transform FRFoot = FRD.GetChild (0).transform;
		controlPoints [9] = FRFoot;

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
}
