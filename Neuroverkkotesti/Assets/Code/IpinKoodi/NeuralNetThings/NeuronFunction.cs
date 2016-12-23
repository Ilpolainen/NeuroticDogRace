using UnityEngine;
using System.Collections;


public abstract class NeuronFunction  {

	// Use this for initialization
	public abstract float Value(float input);

	public abstract float Mutate (float current, float emperature, float volume);
}
