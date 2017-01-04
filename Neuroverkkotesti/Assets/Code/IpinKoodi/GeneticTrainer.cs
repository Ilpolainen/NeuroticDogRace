using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code {

	public class GeneticTrainer : MonoBehaviour {

		public FileManager fileManager;
		public Evaluator evaluator;
		public GameObject model;


		public float timeStep;
		private float currentTime;
		private float lastTime;

		public GameObject[] units;
		public List<Mind> minds;
		private List<NeuralNet> best;

		public int[] hiddenLayers;
		public int populationSize;
		public int eliteCount;
		public int randomElites;
		public float startingNeuronWeightInterval;
		public float startingOutputWeightInterval;
		public float coolingRatio;
		public int mutationCount;
		public int outputMutationCount;
		public float randomZRotation;
		public float randomXRotation;
		public bool load;
		private List<NeuralNet> loaded;

		private float currentNeuronWeightInterval;
		private float currentOutPutWeightInterval;

		// INITIALIZATION

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
			evaluator.SetRandomElites (randomElites);
			currentNeuronWeightInterval = startingNeuronWeightInterval;
			currentOutPutWeightInterval = startingOutputWeightInterval;
			units = Creator.CreateEmptyObjects (populationSize);
			//Creator.AssignTargets (units);
			Creator.AttachHorses (model, units);
			Creator.AttachMinds (units, load, loaded, hiddenLayers);
			CollectNeuralNets ();
			CollectMinds ();

			fileManager.minds = minds;

			evaluator.units = units;
			//print ("FROM TRAINER START: " + units [0].GetComponent<Mind> ().neuralnet.layers[0].neurons[0].weights[0] + " Ready: " + units [0].GetComponent<Mind> ().ready);

		}
	
		//UPDATE

		void Update () {
			
			//print ("FROM TRAINER UPDATE: " + units [0].GetComponent<Mind> ().neuralnet.layers[0].neurons[0].weights[0]);

			//if (currentInterval < 0.01f) {
			//	fileManager.SaveAll ();
			//	Application.Quit ();
			//}
		}

		void FixedUpdate() {
			if (Time.frameCount == 1) {
				Reset();
				RandomRotation ();
			}
			if (currentTime > timeStep) {
				NextGeneration ();
				currentNeuronWeightInterval = currentNeuronWeightInterval * coolingRatio;
				currentOutPutWeightInterval = currentOutPutWeightInterval * coolingRatio;
			}
			float newTime = Time.fixedTime;
			currentTime = currentTime + newTime - lastTime;
			lastTime = newTime;
			//print (currentTime);
		}
		//FUNCTIONS

		private void NextGeneration() 
		{
			evaluator.TurnWhite ();
			best = evaluator.GetNBest(eliteCount);
			evaluator.Show (best);
			Creator.GetNextGeneration (minds,best,currentNeuronWeightInterval,currentOutPutWeightInterval,mutationCount, outputMutationCount);
			Reset();
			RandomRotation ();
			evaluator.neuralNets = CollectNeuralNets ();
			print ("CURRENT NEURONWEIGHT INTERVAL: " + currentNeuronWeightInterval);
			print ("CURRENT OUTPUTWEIGHT INTERVAL: " + currentOutPutWeightInterval);
		}

		private void CollectMinds() {
			for (int i = 0; i < units.Length; i++) {
				this.minds.Add(units [i].GetComponent<Mind> ());
			}
		}

		private NeuralNet[] CollectNeuralNets() {
			NeuralNet[] neuralnets = new NeuralNet[minds.Count];
			for (int i = 0; i < minds.Count; i++) {
				minds [i].neuralnet.SetValue (0);
				neuralnets [i] = minds [i].neuralnet;
			}
			return neuralnets;
		}
			
		private void RandomRotation() {
			float x =  Random.value * randomXRotation - 0.5f * randomXRotation;
			float z = Random.value * randomZRotation - 0.5f * randomZRotation;
			foreach (GameObject unit in this.units) {
				unit.GetComponent<Mind>().GetComponent<Steerable>().physGo.transform.RotateAround (unit.GetComponent<Mind>().GetComponent<Steerable>().physGo.transform.position, Vector3.forward, z);
				unit.GetComponent<Mind>().GetComponent<Steerable>().physGo.transform.RotateAround (unit.GetComponent<Mind>().GetComponent<Steerable>().physGo.transform.position, Vector3.right, x);
				//if (Mathf.Abs (unit.transform.GetChild(0).transform.rotation.eulerAngles.z) * 1.5 > randomZRotation || Mathf.Abs(unit.transform.rotation.eulerAngles.x) * 1.5 > randomXRotation) {
				//	unit.transform.rotation = Quaternion.identity;
				//}
			}
		}

		private void CheckInputParametres() {
			if (load) {
				populationSize = fileManager.storage.neuralNets.Count;
			} else {
				populationSize = Mathf.Abs (populationSize);
			}
			eliteCount = Mathf.Abs (eliteCount);
			coolingRatio = Mathf.Abs (coolingRatio);
			startingNeuronWeightInterval = Mathf.Abs (startingNeuronWeightInterval);
			startingOutputWeightInterval = Mathf.Abs (startingOutputWeightInterval);
			if (populationSize < eliteCount) {
				eliteCount = populationSize;
			}
			if (randomElites > eliteCount / 2) {
				randomElites = eliteCount / 3;
			}
		}

		private void Reset() {
			for (int i = 0; i < units.Length; i++) {
				//Vector3 target = ComponentUtilities.GetSteerablesBallCoords (units[i].GetComponent<Steerable>());
				units [i].GetComponent<Steerable> ().Reset ();
				//units [i].GetComponent<Steerable> ().target.transform.position = target;
				currentTime = 0;
			}
		}



		//--------------------------------------------------------

		// FOR DEBUGGING

		private void Information() {
			for(int i = 0; i < units.Length; i++) {	
				print ("Steerable: " +units [i].GetComponent<Steerable> ().id);
				print ("Mind: " + units [i].GetComponent<Mind> ().id);
				print ("NeuralNet: " + units [i].GetComponent<Mind> ().neuralnet.id);
			}
		}


		//THE HEAP SEEMS TO WORK

		//private void HeapTest() {
		//	NeuralNetComparator comparator = new NNetValueComparator ();
		//	NeuralNetHeap heap = new NeuralNetHeap (20, comparator);
		//	for (int i = 0; i < 20; i++) {
		//		NeuralNet net = new NeuralNet ();
		//		net.value = Random.value;
		//		heap.heapInsert (net);
		//	}
		//	for (int i = 0; i < this.eliteCount; i++) {
		//		print (heap.heapDelmax ().value);
		//	}
		//}
	}
}