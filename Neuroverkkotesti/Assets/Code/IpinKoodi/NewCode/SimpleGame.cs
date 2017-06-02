using UnityEngine;
using System.Collections;

public class SimpleGame : MonoBehaviour {

	public int[] structure;
	GameObject[] creatures;
	// Use this for initialization
	void Start () {
		creatures = GameObject.FindGameObjectsWithTag ("Creature");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
