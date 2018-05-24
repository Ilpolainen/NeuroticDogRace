using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {

    public static Info Instance { get; private set; }
    //
    public Academy academy;
    public Game game;
    public Transform prefab;
    public int unitCount;
    public int[] unitStatuses;
    public int[] statusCounts;
    public int inputSize;
    public int outputSize;
    public int[] hiddenLayerStructure;
    public float randomness;
    public int numberOfNeuronMutations;
    public int netsPerUnit;
    public Quaternion[] eyeRotations;
    public List<GameObject> targets;
    public Transform winner;
    
    // Use this for initialization
	private void Awake () {
        if (Instance == null)
        {
            Instance = this;
            netsPerUnit = 2;
            targets = new List<GameObject>();
            randomness = 3;
            numberOfNeuronMutations = 20;
            statusCounts = new int[5];
            unitCount = 1;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        netsPerUnit = 2;
        randomness = 3;
        numberOfNeuronMutations = 20;
        unitCount = 1;
    }

    public void ResetToInitial()
    {
        unitCount = 1;
        netsPerUnit = 2;
        targets = new List<GameObject>();
        randomness = 3;
        numberOfNeuronMutations = 20;
        statusCounts = new int[5];
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

    public void UpdateStatuses()
    {
        for (int i = 0; i < unitCount;i++)
        {
            unitStatuses[i] = academy.units[i].transform.GetComponentInChildren<IndividualInfo>().status;
        }
        UpdateStatusCounts();
    }

    void UpdateStatusCounts()
    {
        ClearStatusCounts();
        foreach (Transform unit in academy.units)
        {
            //Debug.Log(unit.transform.GetComponentInChildren<IndividualInfo>().status);
            statusCounts[unit.transform.GetComponentInChildren<IndividualInfo>().status] += 1;
            //Debug.Log(statusCounts[unit.transform.GetComponentInChildren<IndividualInfo>().status]);
        }
    }

    public void ClearStatusCounts()
    {
        for (int i = 0; i < 5; i++)
        {
            statusCounts[i] = 0;
        }
    }
}
