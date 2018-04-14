using UnityEngine;
using System.Collections;
namespace Code {

	public class Mind2 : MonoBehaviour {

		public NeuralNet neuralnet;
		private MuscledBody mb;
		public int[] hiddenStructure;
		public bool ready;
		public int id;
		// Use this for initialization
		void Start () {
			mb = this.gameObject.GetComponent<MuscledBody> ();
		}

		// Update is called once per frame
		void FixedUpdate () {
			if (ready) {
				float[] output = neuralnet.GiveOutput (mb.GetSensorInfo ());
                int divider = 1;
                if (hiddenStructure.Length > 0)
                {
                    divider = hiddenStructure[hiddenStructure.Length - 1];
                }	else
                {
                    divider = mb.GetSensorInfo().Length;
                }
				mb.Move (output, divider, 100);
			} else {
				if (mb.ready) {
					neuralnet = defaultNN ();
					ready = true;
				}
			}
		}

		public void SetNeuralNet(NeuralNet net) {
			this.neuralnet = net;
		}

		public void SetMuscledBody(MuscledBody mb) {
			this.mb = mb;
		}

		public MuscledBody GetMuscledBody() {
			return mb;
		}

		public int[] GetStructure() {
			return hiddenStructure;
		}

		public void SetStructure(int[] structure) {
			this.hiddenStructure = structure;
		}

		public void SetId(int id) {
			this.id = id;
		}

		private NeuralNet defaultNN() {
			int inputSize = mb.GetSensorInfo ().Length;
			int outputSize = mb.GetJoints ().Length;
			int[] structure = new int[hiddenStructure.Length + 2];
			structure [0] = inputSize;
			structure [structure.Length - 1] = outputSize;
			for (int i = 0; i < hiddenStructure.Length; i++) {
				structure [i + 1] = hiddenStructure [i];
			}
			NeuralNet first = new NeuralNet (structure);
			return first;
		}

        public void updateNeuralNet(float randomness)
        {
            int inputSize = mb.GetSensorInfo().Length;
            int outputSize = mb.GetJoints().Length;
            int[] structure = new int[hiddenStructure.Length + 2];
            structure[0] = inputSize;
            structure[structure.Length - 1] = outputSize;
            for (int i = 0; i < hiddenStructure.Length; i++)
                {
                    structure[i + 1] = hiddenStructure[i];
                }
            NeuralNet first = new NeuralNet(structure,randomness);
            this.neuralnet = first;
        }
    }

}
