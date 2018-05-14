using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class IndividualInfo : MonoBehaviour {

    
    public List<Transform> parents;
    public ArrayList weightMatrices;
    public int status;
	// Use this for initialization
	void Start () {
        status = 0;
        parents = new List<Transform>();
	}

    public void ClearStatus()
    {
        status = 0;
        parents = new List<Transform>();
    }

    public float GetWeightMean()
    {
        float mean = 0;
        ArrayList weightMatrices = transform.GetComponentInChildren<Mind2>().neuralnet.GetWeightMatrices();
        int num = 0;
        for (int i = 0; i < weightMatrices.Count;i++)
        {
            float[][] layer = (float[][])weightMatrices[i];
            for (int j = 0; j < layer.Length; j++)
            {
                for (int k = 0; k < layer[j].Length; k++)
                {
                    mean = mean + layer[j][k];
                    num += 1;
                }
            }
        }
        mean = mean / num;
        return mean;
    }

    public float GetWeightStandardDeviationFromZero()
    {
        float mean = 0;
        ArrayList weightMatrices = transform.GetComponentInChildren<Mind2>().neuralnet.GetWeightMatrices();
        int num = 0;
        for (int i = 0; i < weightMatrices.Count; i++)
        {
            float[][] layer = (float[][])weightMatrices[i];
            for (int j = 0; j < layer.Length; j++)
            {
                for (int k = 0; k < layer[j].Length; k++)
                {
                    mean = mean + Mathf.Abs(layer[j][k]);
                    num += 1;
                }
            }
        }
        mean = mean / num;
        return mean;
    }


    // Update is called once per frame

}
