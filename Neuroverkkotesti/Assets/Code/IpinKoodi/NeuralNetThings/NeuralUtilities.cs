using UnityEngine;
using System.Collections;


public class NeuralUtilities {


	public static void RandomWeights(float[] weights) {
		for(int i = 0;i <weights.Length;i++) {
			weights [i] = Random.value * 2 - 1;
		}
	}
		
}


