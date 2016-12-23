using UnityEngine;
using System.Collections;

public class NNetValueComparator : NeuralNetComparator {

	public int Compare(NeuralNet n1, NeuralNet n2) 
	{
		if (n1.value > n2.value) {
			return 1;
		}
		if (n1.value < n2.value) {
			return -1;
		}
		return 0;
	}

}
