using UnityEngine;
using System.Collections;

public class Sigmoid {

	public static float Value (float input)
	{
		return 2/(1+Mathf.Exp(-input)) - 1;
	}

	public static float Mutate (float current, float emperature, float volume)
	{
		throw new System.NotImplementedException ();
	}

}
