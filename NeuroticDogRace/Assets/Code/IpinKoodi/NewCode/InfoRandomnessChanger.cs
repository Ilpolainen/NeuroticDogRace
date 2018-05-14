using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewCode;
using UnityEngine.UI;

public class InfoRandomnessChanger : MonoBehaviour {

    public Text text;
	// Use this for initialization
	public void ChangeRandomness(float randomness)
    {
        Info.Instance.randomness = randomness;
        text.text = "" + randomness;
    }
}
