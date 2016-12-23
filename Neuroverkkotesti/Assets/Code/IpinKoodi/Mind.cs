using UnityEngine;
using System.Collections;
namespace Code {

	public class Mind : MonoBehaviour {

		private NeuralNet neuralnet;
		private NeuronFunction function;
		private int[] structure;
		private Steerable steerable;
		int S;
	// Use this for initialization
		void Start () {
			steerable = this.gameObject.GetComponent<Steerable> ();
			S = 0;
		}
	
	// Update is called once per frame
		void Update () {
			if (S < 10) {
				S++;
			} else if (S == 10) {
				SetNeuralFunctionAndStructure ();

				S++;
			} else {
				steerable.Steer (neuralnet.GiveOutput (steerable.GetPositionInfo()));
			}
		}

		public void SetNeuralFunctionAndStructure() 
		{
			structure = new int[3];
			structure [0] = steerable.GetPositionInfo ().Length;
			structure [1] = 8;
			structure [2] = steerable.GetJoints ().Length;
			neuralnet = new NeuralNet (structure, 1);
		}

		public NeuralNet GetNeuralNet() {
			return this.neuralnet;
		}

		public void SetNeuralNet(NeuralNet net) {
			this.neuralnet = net;
		}
	
		public void SetSteerable(Steerable steer) {
			this.steerable = steer;
		}
	}

}
