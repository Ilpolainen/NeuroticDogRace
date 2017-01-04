using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Neuron  {

	[XmlAttribute("neuronWeights")]
	public float[] weights;


	//---------------------------

	//CONSTRUCTORS

	public Neuron() 
	{
	}


	public Neuron(Neuron neuron) {
		this.weights = neuron.GetWeights ();
	}



	public Neuron(int inputSize) {
		weights = new float[inputSize + 1];
		NeuralUtilities.RandomWeights (1, weights);
		//Debug.Log ("FROM NEURON CONSTRUCTOR FIRST WEIGHT" + weights [0]);
		//Debug.Log ("FROM NEURON CONSTRUCTOR LAST WEIGHT" + weights [weights.Length-1]);
	}


	//----------------------------

	//OUTPUTFUNCTION
		

	public float giveOutput(float[] input) {
		float sum = 0;
		if (input.Length != weights.Length - 1) {
			Debug.Log ("Wrong number of neuron inputs!");
		} else {
			for (int i = 0; i < input.Length; i++) {
				sum += input [i] * weights [i] * weights [i] * weights [i];
			}
			sum += weights [input.Length] * weights [input.Length] *  weights [input.Length];
		}
		return NeuralFunction.Value (sum);
	}
		



	//-------------------------

	//COPY AND MUTATEFUNCTIONS



	public void Mutate(int weight,  float volume)
	{
		weights[weight] = NeuralFunction.Mutate (weights[weight], volume);
	}

	public Neuron copyWithMutation(int numberOfMutations, float volume)
	{
		Neuron neuron = new Neuron (this);
		for (int i = 0; i < numberOfMutations; i++) {
			int weight = Random.Range (0, weights.Length - 1);
			weights[weight] = NeuralFunction.Mutate (weights[weight], volume);
		}
		return neuron;
	}

	public Neuron Copy() 
	{
		Neuron neuron = new Neuron (this);
		return neuron;
	}


	//-------------------------

	//GETTERS AND SETTERS

	public float[] GetWeights()
	{
		return weights;
	}




	//---------------------------

	// Update is called once per frame

}
