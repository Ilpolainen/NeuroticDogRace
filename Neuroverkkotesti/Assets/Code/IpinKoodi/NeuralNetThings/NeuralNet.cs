using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("NNet")]
public class NeuralNet {


	[XmlArray("neuronLayers")]
	[XmlArrayItem("layer")]
	public Layer[] layers;

	[XmlArray("netOutputWeights")]
	[XmlArrayItem("weight")]
	public float[] outputWeights;

	[XmlAttribute("Value")]
	public float value;

	[XmlAttribute("Id")]
	public int id;
	private float[] output;

	public NeuralNet()
	{
	}


	public NeuralNet(int[] structure) {
		value = 0;
		layers = new Layer[structure.Length - 1];
		for (int i = 1; i < structure.Length; i++) {
			layers [i - 1] = new Layer(structure [i], structure [i - 1]);
		}
		outputWeights = new float[structure[structure.Length - 1]];
		NeuralUtilities.RandomWeights (3, outputWeights);
	}

	public NeuralNet(NeuralNet neuralnet) {
		value = 0;
		layers = new Layer[neuralnet.GetLayers().Length];
		for (int i = 0; i < layers.Length; i++) {
			layers [i] = new Layer(neuralnet.GetLayers () [i]);
		}
		this.outputWeights = new float[neuralnet.outputWeights.Length];
		for (int i = 0; i < outputWeights.Length; i++) {
			this.outputWeights [i] = neuralnet.outputWeights [i];
		}

		id = neuralnet.id;
	}

	public float[] GiveOutput(float[] input) 
	{
		if (input.Length != layers [0].inputLength) {
			Debug.LogError ("First layer's input size doesn't match fiven input");
			return null;
		} else {
			output = GiveRecursiveOutput (input, layers.Length - 1);
			for (int i = 0; i < output.Length; i++) {
				output [i] = output [i] * outputWeights [i];
			}
			return output;
		}
	}

	public float[] GiveRecursiveOutput(float[] input, int i) {
		if (i == 0) {
			return layers [0].GiveOutPut (input);
		} else {
			return layers [i].GiveOutPut (GiveRecursiveOutput (input, i - 1));
		}
	}

	public NeuralNet Divide(int mutationCount, float neuronVolume, int outPutAmounts, float outputVolume) 
	{
		NeuralNet mutated = new NeuralNet (this);
		for (int i = 0; i < mutationCount - 1; i++) {
			Layer l = mutated.GetLayers () [Random.Range (0, mutated.GetLayers ().Length - 1)];
			Neuron n = l.GetNeurons () [Random.Range (0, l.GetNeurons ().Length - 1)];
			n.Mutate (Random.Range(0,n.weights.Length-1),neuronVolume);
		}
		MutateOutputWeights (mutated, outPutAmounts, outputVolume);
		
		return mutated;
	}

	public Layer[] GetLayers() 
	{
		return this.layers;
	}



	public void MutateOutputWeights(NeuralNet mutated, float count, float volume) {
		for (int i = 0; i < count; i++) {
			int weight = Random.Range (0, outputWeights.Length - 1);
			//Debug.Log((2 * Random.value - 1) * volume);
			mutated.outputWeights [weight] = outputWeights [weight] + ((2*Random.value - 1) * volume);

		}


	}
	// SETTERS AND GETTERS

	public void SetId(int i) {
		id = i;
	}

	public void SetOutputWeights(float[] weights) {
		this.outputWeights = weights;
	}

	public void SetValue(float v) {
		value = v;
	}
}
