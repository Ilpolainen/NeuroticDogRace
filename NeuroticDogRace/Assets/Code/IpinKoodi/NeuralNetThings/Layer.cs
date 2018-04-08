using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("Layer")]
public class Layer {

	[XmlArray ("neurons")]
	[XmlArrayItem("neuron")]
	public Neuron[] neurons;

	public float[] output;
	[XmlAttribute("inputLength")]
	public int inputLength;

    [XmlAttribute("type")]
    public int type;

	public Layer()
	{
	}
		
	public Layer(Layer layer) {
        this.type = layer.type;
        neurons = new Neuron[layer.GetNeurons ().Length];
		for (int i = 0; i < neurons.Length; i++) {
			neurons [i] = new Neuron (layer.GetNeurons () [i],type);
		}
       
		this.inputLength = layer.inputLength;
		this.output = new float[layer.output.Length];
	}

	public Layer(int neuronCount, int inputSize, int type) 
	{
        this.type = type;
		this.inputLength = inputSize;
		this.neurons = new Neuron[neuronCount];
		this.output = new float[neuronCount];
		for (int i = 0; i < neuronCount; i++) {
			neurons [i] = new Neuron (inputSize,type);
		}
	}

	public float[] GiveOutPut(float[] input) {
		for (int i = 0; i < neurons.Length; i++) {
			output [i] = neurons [i].giveOutput (input);
		}
		return output;
	}

	public Neuron[] GetNeurons()
	{
		return neurons;
	}




}
