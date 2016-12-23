using System;
using UnityEngine;
using System.Collections.Generic;

namespace Code
{
	public class MockNet : INeuralNet
	{
		private List<Layer> layers;

		private class Layer{
			private class Neuron{
				public List<float> weights;
				public Neuron(int count){
					weights = new List<float>();
					for(int i = 0; i< count; i++)
					{
						weights.Add(UnityEngine.Random.value * 2 -1);			
					}
				}
			}

			public List<float> results;

			private List<Neuron> neurons;

			public Layer(int inputs, int outputs){
				neurons = new List<Neuron>();
				results = new List<float>(outputs);
				for (int i = 0; i < outputs; i++) {
					neurons.Add(new Neuron(inputs + 1));
					results.Add(0f);
				}

			}

			public List<float> calculate(IList<float> input)
			{
				List<float> result = new List<float> ();
				int c = 0;
				foreach (Neuron n in neurons) {
					float sum = 0;
					for (int i = 0; i < input.Count; i++) {
						sum += n.weights [i] * input [i];
					}
					sum += n.weights [c];
					results [c] = sigmoid (sum);
					c++;
				}
				return result;
			}

			private float sigmoid(float f)
			{
				return (float) (1 / (1 + Mathf.Exp (-f)));
			}

		}


		public IList<float> Output (IList<float> input)
		{
			layers[0].calculate (input);
			for (int i = 1; i<layers.Count; i++) {
				layers[i].calculate (layers [i-1].results);
			}
			return layers [layers.Count - 1].results;
		}

		public MockNet (IList<int> structure)
		{
			layers = new List<Layer> ();
			for (int i = 1; i<structure.Count; i++) {
				Layer layer = new Layer (structure[i-1], structure[i]);
				layers.Add (layer);
			}
		}
	}
}

