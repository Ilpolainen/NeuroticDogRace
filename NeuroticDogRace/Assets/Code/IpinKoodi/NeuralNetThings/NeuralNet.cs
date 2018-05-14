using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("NNet")]
public class NeuralNet {


	[XmlArray("neuronLayers")]
	[XmlArrayItem("layer")]
	public Layer[] layers;

	[XmlAttribute("Value")]
	public float value;

	[XmlAttribute("Id")]
	public int id;
	private float[] output;

	public NeuralNet()
	{
	}


	public NeuralNet(int[] structure, float randomness) {
        //Debug.Log("FROM NEURALNET:");
        //Debug.Log("NEW RANDOM NET");
        value = 0;
		layers = new Layer[structure.Length - 1];
		for (int i = 1; i < structure.Length-1; i++) {
			layers [i - 1] = new Layer(structure [i], structure [i - 1],0,randomness);
		}
        layers[structure.Length - 2] = new Layer(structure[structure.Length - 1], structure[structure.Length - 2], 1,randomness);
	}


	public NeuralNet(NeuralNet neuralnet) {
		value = 0;
		layers = new Layer[neuralnet.GetLayers().Length];
		for (int i = 0; i < layers.Length; i++) {
			layers [i] = new Layer(neuralnet.GetLayers () [i]);
		}
		id = neuralnet.id;
	}

    public NeuralNet(NeuralNet[] parents, int id)
    {
        //Debug.Log("FROM NEURALNET:");
        //Debug.Log("NEW CHILD OF " + parents.Length + " PARENTS!");
        value = 0;
        layers = new Layer[parents[0].GetLayers().Length];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(parents[0].GetLayers()[i]);
        }
        for (int l = 0; l < layers.Length; l++)
        {
            for (int n = 0; n < layers[l].neurons.Length;n++)
            {
                for (int w = 0; w < layers[l].neurons[n].weights.Length; w++)
                {
                    layers[l].neurons[n].weights[w] = 0;
                }
            }
        }
        for (int p = 0; p < parents.Length; p++)
        {
            for (int l = 0; l < layers.Length; l++)
            {
                for (int n = 0; n < layers[l].neurons.Length; n++)
                {
                    for (int w = 0; w < layers[l].neurons[n].weights.Length; w++)
                    {
                        layers[l].neurons[n].weights[w] = layers[l].neurons[n].weights[w] + parents[p].layers[l].neurons[n].weights[w];
                    }
                }
            }
        }
        for (int l = 0; l < layers.Length; l++)
        {
            for (int n = 0; n < layers[l].neurons.Length; n++)
            {
                for (int w = 0; w < layers[l].neurons[n].weights.Length; w++)
                {
                    layers[l].neurons[n].weights[w] = layers[l].neurons[n].weights[w]/2;
                }
            }
        }
        this.id = id;
    }

	public float[] GiveOutput(float[] input) 
	{
		if (input.Length != layers [0].inputLength) {
			Debug.LogError ("First layer's input size doesn't match given input");
			return null;
		} else {
            return GiveRecursiveOutput(input, layers.Length - 1);
        }
	}

	public float[] GiveRecursiveOutput(float[] input, int i) {
		if (i == 0) {
			return layers [0].GiveOutPut (input);
		} else {
			return layers [i].GiveOutPut (GiveRecursiveOutput (input, i - 1));
		}
	}

	public NeuralNet Mutate() 
	{
        //Debug.Log("FROM NEURALNET:");
        //Debug.Log("NEW MUTATED NETWORK!");
		NeuralNet mutated = new NeuralNet (this);
		for (int i = 0; i < Info.Instance.numberOfNeuronMutations; i++) {
			Layer l = mutated.GetLayers () [Random.Range (0, mutated.GetLayers ().Length)];
			Neuron n = l.GetNeurons () [Random.Range (0, l.GetNeurons ().Length)];
			n.Mutate (Random.Range(0,n.weights.Length),Info.Instance.randomness);
		}
		return mutated;
	}

	public Layer[] GetLayers() 
	{
		return this.layers;
	}



	// SETTERS AND GETTERS

	public void SetId(int i) {
		id = i;
	}


    void DebugLastNeuron()
    {
        Layer last = layers[layers.Length - 1];
        Neuron neuron = last.neurons[last.neurons.Length - 1];
    }

    public void SetValue(float v) {
		value = v;
	}

    public ArrayList GetWeightMatrices()
    {
        ArrayList matrices = new ArrayList();
        foreach (Layer layer in layers)
        {
            Neuron[] neurons = layer.neurons;
            int width = neurons[0].GetWeights().Length;
            float[][] weights = new float[neurons.Length][];
            for (int i=0;i <neurons.Length; i++)
            {
                weights[i] = neurons[i].weights;
            }
            matrices.Add(weights);
        }
        return matrices;
    }

}
