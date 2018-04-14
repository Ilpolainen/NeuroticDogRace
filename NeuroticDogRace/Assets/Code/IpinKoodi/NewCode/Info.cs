using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {

    public static Info Instance { get; private set; }
    //
    public Academy academy;
    public Game game;
    public Transform prefab;
    public int units;
    public int[] hiddenLayerStructure;
    public float randomness;
    
    // Use this for initialization
	private void Awake () {
		if (Instance == null)
        {
            Instance = this;
            units = 1;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //DebugStructure();
    }

    public void DebugStructure()
    {
        Debug.Log("FROM INFO");
        string structure = "";
        foreach (int value in hiddenLayerStructure)
        {
            structure = structure + "|" + value;
        }
        Debug.Log(structure);
    }
}
