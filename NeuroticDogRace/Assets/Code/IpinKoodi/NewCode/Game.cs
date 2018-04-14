using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour {

    Creator creator;
    public BuildInstructions buildInstructions;
    public Action currentAction;


    // Use this for initialization
    void Start () {
        ConstructActions();
        Info.Instance.game = this;

	}
	
	// Update is called once per frame
	
        
    void ConstructActions()
    {
        currentAction = gameObject.AddComponent<SetAsChild>();
        gameObject.AddComponent<ChoosingParents>();
        gameObject.AddComponent<MarkDesposable>();
    }


    public Action GetAction()
    {
        return currentAction;
    }
}
