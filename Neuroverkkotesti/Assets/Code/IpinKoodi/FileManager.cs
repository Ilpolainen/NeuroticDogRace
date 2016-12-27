using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Code {
	public class FileManager : MonoBehaviour {

		private List<GameObject> currents;
		public List<Mind> minds;
		public string chosenFileName;
		public string allFileName;
		public string loadFile;
		public NetStorage storage;
	// Use this for initialization
		void Start () {
			
			this.storage = new NetStorage ();
			currents = new List<GameObject> ();
		}
	
	// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown (KeyCode.C)) {
				print ("Saving...");
				SaveChosen ();
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				print ("Saving...");
				SaveAll ();
			}
			if (Input.GetKeyDown (KeyCode.L)) {
				print ("Loading...");
				Load ();
			}
		}

		public void SetCurrents(List<GameObject> gos) 
		{
			this.currents = gos;
		}

		private void SaveChosen()
		{
			List <NeuralNet> nets = new List <NeuralNet>();
			for (int i = 0; i < currents.Count; i++) {
				Mind mind = currents [i].GetComponent<Mind> ();
				NeuralNet net = mind.neuralnet;
				nets.Add(net);
			}
			storage.SetNeuralnets (nets);
			storage.Save (Path.Combine (Application.persistentDataPath, this.chosenFileName + ".xml"));
			print (Application.persistentDataPath);
		}

		private void SaveAll()
		{
			List <NeuralNet> nets = new List <NeuralNet>();
			for (int i = 0; i < minds.Count; i++) {
				
				NeuralNet net = minds[i].neuralnet;
				nets.Add(net);
			}
			storage.SetNeuralnets (nets);
			storage.Save (Path.Combine (Application.persistentDataPath, this.allFileName + ".xml"));
		}

		private void Load() {
			storage  = NetStorage.Load (Path.Combine(Application.persistentDataPath,this.loadFile + ".xml"));
			for(int i = 0;i<Mathf.Max(storage.neuralNets.Count,currents.Count);i++) {
				Mind mind = currents[i].GetComponent<Mind> ();
				mind.SetNeuralNet(storage.GetNetWithIndex(i));
			}
		}

		public void AddToCurrents(GameObject go)
		{
			currents.Add (go);
		}
	}

}