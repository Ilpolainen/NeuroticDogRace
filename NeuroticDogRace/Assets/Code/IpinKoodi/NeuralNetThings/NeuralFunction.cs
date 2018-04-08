using UnityEngine;
using System.Collections;

public class NeuralFunction {

	public static float Value (float input)
	{
		return 2/(1+Mathf.Exp(-input)) - 1;
	}

    public static float Relu(float input)
    {
        if (input > 0)
        {
            return input;
        }
        return 0;
    }

	public static float Mutate (float current, float volume)
	{
		return current + ((2 * Random.value-1) * volume);
	}

	public static NeuralNet Mutate(NeuralNet net, float volume) 
	{
		throw new System.NotImplementedException ();
	}

}
