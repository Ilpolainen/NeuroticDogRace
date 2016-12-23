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

	private float[] output;

	public NeuralNet()
	{
	}


	public NeuralNet(int[] structure, int functionType) {
		value = 0;
		layers = new Layer[structure.Length - 1];
		for (int i = 1; i < structure.Length; i++) {
			layers [i - 1] = new Layer(structure [i], structure [i - 1], functionType);
		}
		outputWeights = new float[structure[structure.Length - 1]];
		NeuralUtilities.RandomWeights (outputWeights);
	}

	public float[] GiveOutput(float[] input) 
	{
		if (input.Length != layers [0].GetInputSize()) {
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


	// SETTERS AND GETTERS



}
