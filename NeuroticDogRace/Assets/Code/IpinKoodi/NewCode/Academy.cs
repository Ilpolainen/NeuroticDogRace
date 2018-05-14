using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;
using NewCode;

public class Academy : MonoBehaviour {

    public Transform[] units;
    public Action currentAction;
    public Transform prefab;
	// Use this for initialization
	void Start () {
        if (Info.Instance != null)
        {
            Info.Instance.prefab = prefab;
            BuildPrefab();
            units = Creator.BuildUnitsInRow(prefab, new Vector3(0, 0.2f, 0), 8);
            prefab.gameObject.SetActive(false);
            Info.Instance.academy = this;
        }
        Vector3 goalOffset = new Vector3(0, 0, -16);
        SetTargetsAndGoal(goalOffset);
        ActivateUnits();
        ResetStatuses();
	}

    void ActivateUnits()
    {
        foreach (Transform unit in units)
        {
            unit.GetComponentInChildren<Mind2>().ready = true;
        }
    }

    void BuildPrefab()
    {
            Mind2 mind = prefab.GetComponentInChildren<Mind2>();
            mind.hiddenStructure = Info.Instance.hiddenLayerStructure;
            mind.RandomizeNeuralNets(Info.Instance.randomness);
    }
    // Update is called once per frame

    

    void SetTargetsAndGoal(Vector3 offset)
    {
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        GameObject apple = GameObject.FindGameObjectWithTag("Target");
        goal.transform.position = offset;
        offset = offset - 2* Vector3.forward;
        foreach (Transform unit in units)
        {
            GameObject newTarget = GameObject.Instantiate(GameObject.FindGameObjectWithTag("Target"),unit.position+offset,unit.rotation);
            unit.GetComponentInChildren<Doggy>().SetTarget(newTarget);
        }
        GameObject.Destroy(apple);
    }

    public void NextGeneration()
    {
        if (Info.Instance.game.winnerFound)
        {
            return;
        }
        Breed();
        HandleRest();
        ResetPositions();
        ResetStatuses();
        Info.Instance.game.SetWeakCommand();
        //DebugMeans();
        //DebugStandardDeviationsFromZero();
    }


    void ResetPositions()
    {
        foreach (Transform unit in units)
        {
            unit.GetComponentInChildren<Doggy>().Reset();
        }
    }


    void Breed()
    {
        foreach (Transform unit in units)
        {
            IndividualInfo unitInfo = unit.GetComponentInChildren<IndividualInfo>();
            if (unitInfo.status==2)
            {
                if (unitInfo.parents.Count ==0)
                {
                    unitInfo.status = 1;
                    return;
                }    
                NeuralNet[] parNets = new NeuralNet[unitInfo.parents.Count];
                Transform[] parTrans = unitInfo.parents.ToArray();
                for (int i = 0; i < parNets.Length; i++)
                {
                    parNets[i] = parTrans[i].GetComponentInChildren<Mind2>().neuralnet;
                }
                Mind2 childsMind = unit.gameObject.GetComponentInChildren<Mind2>();
                NeuralNet[][] copiesOfParnets = new NeuralNet[Info.Instance.netsPerUnit][];
                for (int i = 0; i < Info.Instance.netsPerUnit; i++)
                {
                    copiesOfParnets[i] = new NeuralNet[unitInfo.parents.Count];
                    for (int j = 0; j < unitInfo.parents.Count;j++)
                    {
                        copiesOfParnets[i][j]= parTrans[j].GetComponentInChildren<Mind2>().stages[i];
                    }
                   
                }
                childsMind.Breed(copiesOfParnets);
                Info.Instance.UpdateStatuses();
            }
        }
    }

    void HandleRest()
    {
        List<Transform> disposables = new List<Transform>();
        List<Transform> mutating = new List<Transform>();


        foreach (Transform unit in units)
        {
            IndividualInfo inf = unit.GetComponentInChildren<IndividualInfo>();
           
            if (inf.status == 1)
            {
                disposables.Add(unit);
            }         
            if (inf.status == 4)
            {
                mutating.Add(unit);
            }
        }
        Info.Instance.UpdateStatuses();

        if (disposables.Count != 0)
        {
            Transform[] disposablesArray = disposables.ToArray();
        }
        if (mutating.Count != 0)
        {
            Transform[] mutatingArray = mutating.ToArray();
        }

        for (int i = 0; i < disposables.Count; i++)
        {
            disposables[i].GetComponent<Mind2>().RandomizeNeuralNets(Info.Instance.randomness);
        }
        for (int i = 0; i < mutating.Count; i++)
        {
            mutating[i].GetComponent<Mind2>().Mutate();
        }
    }

    void ResetStatuses()
    {
        foreach (Transform unit in units)
        {
            unit.GetComponentInChildren<IndividualInfo>().ClearStatus();
        }
        Info.Instance.UpdateStatuses();
        ResetColors();
    }

    void ResetColors()
    {
        foreach (Transform unit in units)
        {
            Renderer[] renderers = unit.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                if (rend.gameObject.tag != "Eye")
                {
                    Material[] mats = rend.materials;
                    foreach (Material mat in mats)
                    {
                        mat.color = Color.red;
                    }
                }else
                {
                    Material[] mats = rend.materials;
                    foreach (Material mat in mats)
                    {
                        mat.color = Color.white;
                    }
                }
                
            }
        }
        
    }
    

    void DebugMeans()
    {
        foreach (Transform unit in units)
        {
            Debug.Log(unit.GetComponentInChildren<IndividualInfo>().GetWeightMean());
        }
    }


    void DebugStandardDeviationsFromZero()
    {
        foreach (Transform unit in units)
        {
            Debug.Log(unit.GetComponentInChildren<IndividualInfo>().GetWeightStandardDeviationFromZero());
        }
    }

}
