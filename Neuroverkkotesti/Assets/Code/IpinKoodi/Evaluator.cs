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
		public float legTouchWeight;
		public float targetWeight;

		private int randomElites;


		// Use this for initialization
		void Start () {
			comparator = new NNetValueComparator();
		}
	
		// Update is called once per frame
		void Update () {
			if (Time.frameCount < 3) {
				return;
			} else {
				if (Time.frameCount == 3) {
					neuralNets = new NeuralNet[units.Length];
					steerables = new Steerable[units.Length];
					CollectData ();
				} else if (units[units.Length-1].GetComponent<Mind>().ready) {
					UpdateThings ();
				}
			}
		}

		private void UpdateThings() 
		{
			for (int i = 0; i < units.Length; i++) {
				//MAX HEIGHTS
				neuralNets[i].value = neuralNets[i].value + (steerables [i].GetPositionInfo ()[0]*controlpointOneWeight + steerables[i].GetPositionInfo()[1]*controlpointTwoWeight + (steerables[i].GetPositionInfo()[2]-steerables [i].GetPositionInfo ()[0])*controlpointThreeWeight);
				//MAX FOURLEGGED
				neuralNets[i].value = neuralNets[i].value - (steerables[i].touches[0].touching + steerables[i].touches[1].touching + steerables[i].touches[2].touching + steerables[i].touches[3].touching) *legTouchWeight;
			}
			//print ("FIRST UNIT DISTANCE FROM TARGET: " + (Mathf.Sqrt (steerables [0].GetPositionInfo () [3] * steerables [0].GetPositionInfo () [3] + steerables [0].GetPositionInfo () [4] * steerables [0].GetPositionInfo () [4])));
			//print ("last UNIT DISTANCE FROM TARGET: " + (Mathf.Sqrt (steerables [100].GetPositionInfo () [3] * steerables [100].GetPositionInfo () [3] + steerables [100].GetPositionInfo () [4] * steerables [100].GetPositionInfo () [4])));
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
				//neuralNets [i].SetValue (neuralNets [i].value - (Mathf.Sqrt (steerables [i].GetPositionInfo () [3] * steerables [i].GetPositionInfo () [3] + steerables [i].GetPositionInfo () [4] * steerables [i].GetPositionInfo () [4])) * targetWeight*40/Time.timeScale);
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

		public void SetRandomElites(int amount) {
			this.randomElites = amount;
		}
	
		public void SetNeuralNets(NeuralNet[] nets) {
			neuralNets = nets;
		}
	}
}