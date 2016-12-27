using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code {

	public class Creator : MonoBehaviour {

		public GameObject model;
		public int[] hiddenLayers;

	// Use this for initialization
		void Start () {
		
		}

	// Update is called once per frame
		void Update () {
	
		}



		public GameObject[] CreateEmptyObjects(int size) {
			GameObject[] list = new GameObject[size];
			int rows = 10;
			for (int i = 0; i < Mathf.Max(size/rows,1); i++) {
				for (int j = 0; j < rows; j++) {
					if (i * rows + j >= size) {
						return list;
					}
					Vector3 position = new Vector3 (j * 12, 4, -10 * i);
					GameObject thing = new GameObject ();
					thing.tag = "Thing";
					thing.transform.position = position;
					list [i * rows + j] = thing;

				}
			}
			return list;
		}

		public void AttachHorses(GameObject[] list) 
		{
			for (int i = 0; i < list.Length; i++) {
				list [i].AddComponent<Steerable>();
				list [i].GetComponent<Steerable> ().physGo = GameObject.Instantiate (model,list[i].transform.position,Quaternion.identity) as GameObject;
				list [i].GetComponent<Steerable> ().id = i;
			}
		}

		public void AttachMinds(GameObject[] list)
		{
			for (int i = 0; i < list.Length; i++) {
				list [i].AddComponent<Mind>();
				list [i].GetComponent<Mind> ().hiddenLayers = hiddenLayers;
				list [i].GetComponent<Mind> ().id = i;
			}
		}

		public void GetNextGeneration(List<Mind> minds, List<NeuralNet> best, float volume)
		{

			bool[] marked = new bool[minds.Count];
			foreach (NeuralNet net in best) {
				marked [net.id] = true;
				net.value = 0;
			}

			for (int i = 0; i < minds.Count-best.Count; i++) {
				if (!marked [i]) {
					minds [i].SetNeuralNet (best[i % best.Count].Divide(80,volume,1,volume*5));
					minds [i].neuralnet.id = i;
				}
			}
			for(int i = minds.Count - best.Count;i < minds.Count; i++) {
				if (!marked [i]) {
					minds [i].SetNeuralNet (new NeuralNet(minds[0].structure));
					minds [i].neuralnet.id = i;
				}
			}
		}
	}
}