using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Neuron  {

	[XmlAttribute("neuronWeights")]
	public float[] weights;
	[XmlAttribute("function")]
	public int functionType;

	//---------------------------

	//CONSTRUCTORS

	public Neuron() 
	{
	}


	public Neuron(Neuron neuron) {
		this.weights = neuron.GetWeights ();
		this.functionType = neuron.GetFunctionType ();
	}



	public Neuron(int inputSize, int functionType) {
		weights = new float[inputSize + 1];
		NeuralUtilities.RandomWeights (weights);
		this.functionType = functionType;
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
		return Sigmoid.Value (sum);
	}
		



	//-------------------------

	//COPY AND MUTATEFUNCTIONS



	public void Mutate(int weight, float temperature, float volume)
	{
		weights[weight] = Sigmoid.Mutate (weight, temperature, volume);
	}

	public Neuron copyWithMutation(int numberOfMutations, float temperature, float volume)
	{
		Neuron neuron = new Neuron (this);
		for (int i = 0; i < numberOfMutations; i++) {
			int weight = Random.Range (0, weights.Length - 1);
			weights[weight] = Sigmoid.Mutate (weights[weight], temperature, volume);
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

	public int GetFunctionType()
	{
		return functionType;
	}


	//---------------------------

	// Update is called once per frame

}
