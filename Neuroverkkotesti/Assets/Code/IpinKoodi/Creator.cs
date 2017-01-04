using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code {

	public class Creator {


		public static GameObject[] CreateEmptyObjects(int size) {
			GameObject[] list = new GameObject[size];
			int rows = 10;
			for (int i = 0; i < Mathf.Max(size/rows,1); i++) {
				for (int j = 0; j < rows; j++) {
					if (i * rows + j >= size) {
						return list;
					}
					Vector3 position = new Vector3 (j * 12, 5, -10 * i);
					GameObject thing = new GameObject ();
					thing.tag = "Thing";
					thing.transform.position = position;
					list [i * rows + j] = thing;

				}
			}
			return list;
		}

		public static void AttachHorses(GameObject model, GameObject[] units) 
		{
			for (int i = 0; i < units.Length; i++) {
				units [i].AddComponent<Steerable>();
				units [i].GetComponent<Steerable> ().physGo = GameObject.Instantiate (model,units[i].transform.position,Quaternion.identity) as GameObject;
				units [i].GetComponent<Steerable> ().id = i;
				units [i].GetComponent<Steerable>().Initialize ();
				//units [i].GetComponent<Steerable> ().target = units [i].transform.GetChild (0).gameObject;
			}
		}

		public static void AssignTargets(GameObject[] units) {
			for (int i = 0; i < units.Length; i++) {
				GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				ball.GetComponent<Collider> ().enabled = false;
				ball.AddComponent<ObjectController> ();
				ball.GetComponent<Renderer> ().material.color = Color.green;
				ball.transform.SetParent (units [i].transform);
				ball.transform.localPosition = Vector3.zero;
			}
		}

		public static void AttachMinds(GameObject[] units, bool load, List<NeuralNet> loaded, int[] hiddenLayers)
		{
			if (load) {
				hiddenLayers = new int [loaded[0].GetLayers().Length - 2];
				for(int j = 0; j < hiddenLayers.Length; j++) {
					hiddenLayers[j] = loaded[0].GetLayers()[j+1].GetNeurons().Length;
				}
			}
			for (int i = 0; i < units.Length; i++) {
				units [i].AddComponent<Mind> ();
				units [i].GetComponent<Mind> ().SetStructure (ConvertHiddenLayersToStructure(units[0], hiddenLayers));
				units [i].GetComponent<Mind> ().SetId(i);
				units [i].GetComponent<Mind> ().SetSteerable (units [i].GetComponent<Steerable> ());
				if (load) {
					loaded [i].SetId (i);
					units [i].GetComponent<Mind> ().SetNeuralNet(loaded [i]);
				} else {
					NeuralNet neuralnet = new NeuralNet (ConvertHiddenLayersToStructure(units[0], hiddenLayers));
					neuralnet.SetId(units [i].GetComponent<Mind> ().id);
					units [i].GetComponent<Mind> ().SetNeuralNet(neuralnet);
				}
				units [i].GetComponent<Mind> ().GetSteerable ().ready = true;
				units [i].GetComponent<Mind> ().ready = true;
			}
		}

		public static int[] ConvertHiddenLayersToStructure(GameObject unit, int[] hiddenLayers) 
		{
			int inputSize = unit.GetComponent<Steerable> ().GetPositionInfo ().Length;
			int outputSize = unit.GetComponent<Steerable> ().GetJoints ().Length;
			int[] structure = new int[hiddenLayers.Length + 2];
			structure [0] = inputSize;
			structure [structure.Length - 1] = outputSize;
			for(int i = 1; i < structure.Length - 1; i++) {
				structure [i] = hiddenLayers [i - 1];
			}
			return structure;
		}

		public static void GetNextGeneration(List<Mind> minds, List<NeuralNet> best, float volume, float outputVolume, int neuronMutationCount,int outputMutationCount)
		{

			bool[] marked = new bool[minds.Count];
			foreach (NeuralNet net in best) {
				marked [net.id] = true;
				net.value = 0;
			}

			for (int i = 0; i < minds.Count-best.Count; i++) {
				if (!marked [i]) {
					minds [i].SetNeuralNet (best[i % best.Count].Divide(neuronMutationCount,volume,outputMutationCount,outputVolume));
					minds [i].neuralnet.id = i;
				}
			}
			//for(int i = minds.Count - best.Count/4;i < minds.Count; i++) {
			//	if (!marked [i]) {
			//		minds [i].SetNeuralNet (new NeuralNet(minds[0].structure));
			//		minds [i].neuralnet.id = i;
			//	}
			//}
		}
	}
}