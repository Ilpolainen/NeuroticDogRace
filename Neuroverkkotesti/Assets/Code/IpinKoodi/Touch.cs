using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {

	public float touching;
	// Use this for initialization
	void Start () {
		touching = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollision(Collision col) {
		touching = 10;
	}

	void OnCollisionExit(Collision col) {
		touching = 0;
	}
}
