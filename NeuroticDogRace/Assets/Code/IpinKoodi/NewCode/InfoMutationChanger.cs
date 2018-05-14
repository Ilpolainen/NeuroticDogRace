using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;
using UnityEngine.UI;

public class InfoMutationChanger : MonoBehaviour {

    // Use this for initialization
    public Text text;
    // Use this for initialization
    public void ChangeMutationCount (float mutations)
    {
        Info.Instance.numberOfNeuronMutations = (int) mutations;
        text.text = "" + mutations;
    }
}
