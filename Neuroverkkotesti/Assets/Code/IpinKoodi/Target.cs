using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
		if (Input.GetKeyDown (KeyCode.U)) {
			newPos += Vector3.forward;
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			newPos += Vector3.back;
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			newPos += Vector3.left;
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			newPos += Vector3.right;
		}
		transform.localPosition = newPos;
	}
}
