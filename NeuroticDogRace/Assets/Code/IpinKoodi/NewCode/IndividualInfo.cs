using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualInfo : MonoBehaviour {

    public bool isParent;
    public bool isChild;
    public bool isDisposable;
    public Transform parent;
	// Use this for initialization
	void Start () {
        isParent = false;
        isChild = false;
        isDisposable = false;
        parent = null;
	}
	
	// Update is called once per frame
	
}
