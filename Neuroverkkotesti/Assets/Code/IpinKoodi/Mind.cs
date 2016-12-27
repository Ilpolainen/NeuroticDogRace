using UnityEngine;
using System.Collections;
namespace Code {

	public class Mind : MonoBehaviour {

		public NeuralNet neuralnet;
		private NeuralFunction function;
		public int[] structure;
		public int[] hiddenLayers;
		private Steerable steerable;
		private bool ready;
		public int id;
	// Use this for initialization
		void Start () {
			steerable = this.gameObject.GetComponent<Steerable> ();
			ready = false;
		}
	
	// Update is called once per frame
		void FixedUpdate () {
			if (!ready) {
				SetNeuralFunctionAndStructure ();
				ready = true;
			} else {
				steerable.Steer (neuralnet.GiveOutput (steerable.GetPositionInfo()));
			}
		}

		public void SetNeuralFunctionAndStructure() 
		{
			if (hiddenLayers == null) {
				structure = new int[3];
				structure [1] = 8;
			} else {
				structure = new int[hiddenLayers.Length + 2];
				for (int i = 0; i < hiddenLayers.Length; i++) {
					structure [i + 1] = hiddenLayers [i];
				}
			}

			structure [0] = steerable.GetPositionInfo ().Length;
			structure [structure.Length - 1] = steerable.GetJoints ().Length;
			neuralnet = new NeuralNet (structure);
			neuralnet.id = id;
		}

		public void SetNeuralNet(NeuralNet net) {
			this.neuralnet = net;
		}
	
		public void SetSteerable(Steerable steer) {
			this.steerable = steer;
		}
	}

}
