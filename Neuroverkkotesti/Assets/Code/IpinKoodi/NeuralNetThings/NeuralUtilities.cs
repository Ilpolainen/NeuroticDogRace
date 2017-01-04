using UnityEngine;
using System.Collections;


public class NeuralUtilities {


	public static void RandomWeights(float initializeRange, float[] weights) {
		for(int i = 0;i <weights.Length;i++) {
			weights [i] = Random.value * initializeRange - 0.5f * initializeRange;
		}
	}
		
}


