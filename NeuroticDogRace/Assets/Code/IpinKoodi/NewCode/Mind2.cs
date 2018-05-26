using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Code {

	public class Mind2 : MonoBehaviour {

        Text debug;
		public NeuralNet neuralnet;
        public NeuralNet[] stages;
		private MuscledBody mb;
		public int[] hiddenStructure;
		public bool ready;
		public int id;
        float[] output;
        // Use this for initialization
        void Start () {
			mb = this.gameObject.GetComponent<MuscledBody> ();
            stages = new NeuralNet[Info.Instance.netsPerUnit];
            stages[0] = defaultNN();
            stages[1] = defaultNN();
        }

		// Update is called once per frame
		void FixedUpdate () {
			if (ready && mb.ready) {
                ChooseNet();
				output = neuralnet.GiveOutput (mb.GetSensorInfo ());
                int divider = 1;
                if (hiddenStructure.Length > 0)
                {
                    divider = hiddenStructure[hiddenStructure.Length - 1];
                }	else
                {
                    divider = mb.GetSensorInfo().Length;
                }
				mb.Move (output, divider, 100); 
			}
		}

		public void SetNeuralNet(NeuralNet net) {
			this.neuralnet = net;
		}

        public void Breed(NeuralNet[][] parStages)
        {
            int dummy = 1;
            for (int i = 0; i< stages.Length;i++)
            {
                stages[i] = new NeuralNet(parStages[i],dummy);
            }
        }

        public void ChooseNet()
        {
            // DUMMY
            int net = (int)(Mathf.Max(0, (Mathf.Ceil(Mathf.Sin((mb.eyeSensors[1].distance - mb.eyeSensors[0].distance) * 30)))));
            neuralnet = stages[net];
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
            int inputSize = Info.Instance.inputSize;
			int outputSize = Info.Instance.outputSize;
			int[] structure = new int[hiddenStructure.Length + 2];
			structure [0] = inputSize;
			structure [structure.Length - 1] = outputSize;
			for (int i = 0; i < hiddenStructure.Length; i++) {
				structure [i + 1] = hiddenStructure [i];
			}
			NeuralNet first = new NeuralNet (structure,2);
			return first;
		}

        public void RandomizeNeuralNets(float randomness)
        {

            if (mb == null)
            {
                mb = gameObject.GetComponent<MuscledBody>();
            }
            int inputSize = mb.GetSensorInfo().Length;
            int outputSize = mb.GetJoints().Length;
            int[] structure = new int[hiddenStructure.Length + 2];
            structure[0] = inputSize;
            structure[structure.Length - 1] = outputSize;
            if (stages == null)
            {
                stages = new NeuralNet[Info.Instance.netsPerUnit];
            }
            for (int i = 0; i < hiddenStructure.Length; i++)
                {
                    structure[i + 1] = hiddenStructure[i];
                }
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i] = new NeuralNet(structure, randomness);
            }
            ChooseNet();
        }

        public void Mutate()
        {
            foreach (NeuralNet net in stages)
            {
                net.Mutate();
            }
        }
    }

}
