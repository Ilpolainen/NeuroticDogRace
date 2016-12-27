using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code {

	public class Evaluator : MonoBehaviour {

		public GameObject[] units;
		private Steerable[] steerables;
		public NeuralNet[] neuralNets;
		NeuralNetComparator comparator;

		public float controlpointOneWeight;
		public float controlpointTwoWeight;
		public float controlpointThreeWeight;
		public int randomElites;


		// Use this for initialization
		void Start () {
			comparator = new NNetValueComparator();
		}
	
		// Update is called once per frame
		void Update () {
			if (Time.frameCount < 2) {
				return;
			} else {
				if (Time.frameCount == 2) {
					neuralNets = new NeuralNet[units.Length];
					steerables = new Steerable[units.Length];
					CollectData ();
				} else {
					UpdateThings ();
				}
			}
		}

		private void UpdateThings() 
		{
			//steerables [0].PrintValueMeasures ();
			for (int i = 0; i < units.Length; i++) {
				//print (steerables [i].GetPositionInfo () [1]);
				neuralNets[i].value = neuralNets[i].value + (steerables [i].GetPositionInfo ()[0]*controlpointOneWeight + steerables[i].GetPositionInfo()[1]*controlpointTwoWeight + steerables[i].GetPositionInfo()[2]*controlpointThreeWeight);

			}
			//print ("NeuralNet: " + neuralNets [0] + ", Value: " + neuralNets[0].value);
		}

		private void CollectData() 
		{
			for (int i = 0; i < units.Length; i++) {
				steerables [i] = units [i].GetComponent<Steerable> ();
				neuralNets[i] = units[i].GetComponent<Mind>().neuralnet;
			}
		}



	public List<NeuralNet> GetNBest(int amount) 
		{
			NeuralNetHeap heap = new NeuralNetHeap (units.Length, comparator);
			for (int i = 0; i < neuralNets.Length; i++) {
				heap.heapInsert (neuralNets[i]);
				//Information(i);
			}
			//print ("SHOULD BE THE BEST ONES: ");
			List<NeuralNet> best = new List<NeuralNet>();
			for (int i = 0; i < amount - randomElites; i++) {
				NeuralNet net = heap.heapDelmax ();
				if (i == 0) {
					print (net.value);
				}
			//	Information (net.id);
				best.Add (net);
			}
			for(int i = 0; i < randomElites; i++) {
				for(int j = 1; j < Random.Range(0,heap.getHeapSize()-randomElites-i); j++) {
					heap.heapDelmax ();
				}
				best.Add(heap.heapDelmax());
			}
			return best;
		}


		public void TurnWhite() {
			foreach (Steerable st in steerables) {
				st.physGo.GetComponentInChildren<Renderer> ().material.color = Color.white;
			}
		}


		public void Show(List<NeuralNet> nets) 
		{
			foreach(NeuralNet net in nets) {
				steerables[net.id].physGo.GetComponentInChildren<Renderer> ().material.color = Color.black;
			}
		}
	
		private void Information(int i) {
			print ("Steerable: " + steerables[i].id);
			print ("NeuralNet: " + neuralNets[i].id);
			print ("Value: " + neuralNets[i].value);
		}
	
	}
}