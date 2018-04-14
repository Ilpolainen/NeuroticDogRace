using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class Academy : MonoBehaviour {

    public Transform[] units;
    public Action currentAction;
    public Transform prefab;
	// Use this for initialization
	void Start () {
        Info.Instance.prefab = prefab;
        BuildPrefab();
        units = new Transform[Info.Instance.units];
        for (int unit = 0;unit < Info.Instance.units;unit++)
        {
            Creator.ConstructDog(prefab, new Vector3((float)unit*6, 0.5f, 0),Vector3.zero);
        }
        prefab.gameObject.SetActive(false);
        Info.Instance.academy = this;
	}

    void BuildPrefab()
    {
        if (Info.Instance != null)
        {
            Mind2 mind = prefab.GetComponentInChildren<Mind2>();
            mind.hiddenStructure = Info.Instance.hiddenLayerStructure;
            mind.updateNeuralNet(Info.Instance.randomness);
        }
    }
	// Update is called once per frame
	

    
}
