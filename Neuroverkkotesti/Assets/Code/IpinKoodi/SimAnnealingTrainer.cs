using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code{

	public class SimAnnealingTrainer : MonoBehaviour {

		public FileManager fileManager;
		public Evaluator evaluator;
	
		public GameObject model;
	
		public GameObject[] units;
		public List<Mind> minds;
		private NeuralNet[] stored;
	
		public int[] hiddenLayers;
		public int populationSize;

		public float timeStep;
		private float currentTime;
		private float lastTime;

		public float startingNeuronWeightInterval;
		public float startingOutputWeightInterval;
		public float weightCoolingRatio;

		public float startingTemperature;
		private float currentTemperature;
		public float temperatureCoolingRatio;


		public int mutationCount;
		public int outputMutationCount;
		public float randomZRotation;
		public float randomXRotation;
		public bool load;
		private List<NeuralNet> loaded;
	
		private float currentNeuronWeightInterval;
		private float currentOutPutWeightInterval;
			// Use this for initialization
		void Start () {
			lastTime = Time.fixedTime;
			if (load) {
				loaded = fileManager.Load ();
				hiddenLayers = new int[loaded[0].layers.Length - 1];
				for (int i = 1; i < loaded [0].GetLayers ().Length - 1; i++) {
					hiddenLayers [i] = loaded [0].GetLayers () [i].GetNeurons ().Length;
				}
				populationSize = loaded.Count;
			}
			CheckInputParametres ();
			currentNeuronWeightInterval = startingNeuronWeightInterval;
			currentOutPutWeightInterval = startingOutputWeightInterval;
			currentTemperature = startingTemperature;

			units = Creator.CreateEmptyObjects (populationSize);
			Creator.AttachHorses (model, units);
			Creator.AttachMinds (units, load, loaded, hiddenLayers);
			CollectMinds ();
			stored = CollectNeuralNets ();
			fileManager.minds = minds;
			evaluator.units = units;
		}


		
		// Update is called once per frame
		void Update () {
			
			if (Time.frameCount == 1) {
				Reset();
			}
			if (currentTime > timeStep) {
				currentTime = 0;
				NextGeneration ();
				Debug.Log (minds [0].neuralnet.GetLayers () [1].GetNeurons () [0].weights [0]);
				Debug.Log (minds [10].neuralnet.GetLayers () [1].GetNeurons () [0].weights [0]);
				Debug.Log (minds [100].neuralnet.GetLayers () [1].GetNeurons () [0].weights [0]);
				Reset ();
				currentNeuronWeightInterval = currentNeuronWeightInterval * weightCoolingRatio;
				currentOutPutWeightInterval = currentOutPutWeightInterval * weightCoolingRatio;
				currentTemperature = currentTemperature * temperatureCoolingRatio;
			}
		}

		void FixedUpdate() {
			float newTime = Time.fixedTime;
			currentTime = currentTime + newTime - lastTime;
			lastTime = newTime;
			//print (currentTime);
		}

		private void NextGeneration () {
			Info ();
			for (int i = 0; i < stored.Length; i++) {
				if (stored [i].value <= minds[i].neuralnet.value) {
					stored [i] =  minds[i].neuralnet;
				} else if (Accepted (stored [i].value -  minds[i].neuralnet.value)) {
					print ("HERE");
					stored [i] =  minds[i].neuralnet;
				}
			}
			for (int i = 0; i < minds.Count; i++) {
				minds[i].SetNeuralNet(stored[i].Divide(mutationCount,currentNeuronWeightInterval,outputMutationCount,currentOutPutWeightInterval));
			}
			NeuralNet[] nets = CollectNeuralNets ();
			evaluator.SetNeuralNets (nets);
		}

		private bool Accepted(float penalty) {
			if (Random.value < 1/Mathf.Exp(penalty/currentTemperature)) {
				return true;
			}
			return false;
		}



		private void CollectMinds() {
			for (int i = 0; i < units.Length; i++) {
				this.minds.Add(units [i].GetComponent<Mind> ());
			}
		}

		private NeuralNet[] CollectNeuralNets() {
			NeuralNet[] neuralnets = new NeuralNet[minds.Count];
			for (int i = 0; i < minds.Count; i++) {
				neuralnets [i] = minds [i].neuralnet;
			}
			return neuralnets;
		}

		private void RandomRotation() {
			foreach (GameObject unit in this.units) {
				unit.transform.RotateAround (unit.transform.position, Vector3.forward, Random.value * randomZRotation);
				unit.transform.RotateAround (unit.transform.position, Vector3.right, Random.value * randomXRotation);
				if (Mathf.Abs (unit.transform.rotation.eulerAngles.z) * 1.5 > randomZRotation || Mathf.Abs(unit.transform.rotation.eulerAngles.x) * 1.5 > randomXRotation) {
					unit.transform.rotation = Quaternion.identity;
				}
			}
		}

		private void CheckInputParametres() {
			if (load) {
				populationSize = fileManager.storage.neuralNets.Count;
			} else {
				populationSize = Mathf.Abs (populationSize);
			}

			weightCoolingRatio = Mathf.Abs (weightCoolingRatio);
			startingNeuronWeightInterval = Mathf.Abs (startingNeuronWeightInterval);
			startingOutputWeightInterval = Mathf.Abs (startingOutputWeightInterval);
		}

		private void Reset() {
			for (int i = 0; i < units.Length; i++) {
				units [i].GetComponent<Steerable> ().Reset ();
			}
		}

		private void Info() {
			Debug.Log ("STORED VALUE: " + stored [0].value);
			Debug.Log ("NEW VALUE: " + minds [0].neuralnet.value);
			Debug.Log ("CURRENT TEMPERATURE: " +currentTemperature);
			Debug.Log ("CURRENT NEURON WEIGHT INTERVAL:  " + currentNeuronWeightInterval);
			Debug.Log ("CURRENT OUTPUT WEIGHT INTERVAL: " + currentOutPutWeightInterval);
		}
	}
}
