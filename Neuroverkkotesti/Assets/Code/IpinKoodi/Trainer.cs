using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code {

	public class Trainer : MonoBehaviour {

		public GameObject[] units;
		public List<Mind> minds;
		public FileManager fileManager;
		private Creator creator;
		public Evaluator evaluator;
		private List<NeuralNet> best;

		public int[] hiddenLayers;
		public int populationSize;
		public int eliteCount;
		public float startingInterval;
		public float coolingRatio;

		private float currentInterval;

		// INITIALIZATION

		void Start () {
			CheckInputParametres ();
			currentInterval = startingInterval;

			creator = this.gameObject.GetComponent<Creator> ();
			units = creator.CreateEmptyObjects (populationSize);
			creator.AttachHorses (units);
			creator.AttachMinds (units);
			CollectMinds ();
			fileManager.minds = minds;
			evaluator.units = units;

		}
	
		//UPDATE

		void Update () {
			if (Time.frameCount == 0) {
				Reset();
			}
			if (Time.frameCount % 20 == 0 && Time.frameCount != 0) {
				NextGeneration ();
				currentInterval = currentInterval * coolingRatio;
			}
		}

		//FUNCTIONS

		private void NextGeneration() 
		{
			evaluator.TurnWhite ();
			best = evaluator.GetNBest(eliteCount);
			evaluator.Show (best);
			creator.GetNextGeneration (minds,best,currentInterval);
			Reset();
			evaluator.neuralNets = CollectNeuralNets ();
			print ("CURRENT INTERVAL: " + currentInterval);
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
			
		private void CheckInputParametres() {
			populationSize = Mathf.Abs (populationSize);
			eliteCount = Mathf.Abs (eliteCount);
			coolingRatio = Mathf.Abs (coolingRatio);
			startingInterval = Mathf.Abs (startingInterval);
			if (populationSize < eliteCount) {
				eliteCount = populationSize;
			}

		}

		private void Reset() {
			for (int i = 0; i < units.Length; i++) {
				units [i].GetComponent<Steerable> ().Reset ();
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

		private void HeapTest() {
			NeuralNetComparator comparator = new NNetValueComparator ();
			NeuralNetHeap heap = new NeuralNetHeap (20, comparator);
			for (int i = 0; i < 20; i++) {
				NeuralNet net = new NeuralNet ();
				net.value = Random.value;
				heap.heapInsert (net);
			}
			for (int i = 0; i < this.eliteCount; i++) {
				print (heap.heapDelmax ().value);
			}
		}
	}
}