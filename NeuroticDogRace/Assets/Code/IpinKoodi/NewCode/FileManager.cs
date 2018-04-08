using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Code {
	public class FileManager : MonoBehaviour {

		private List<GameObject> currents;
		
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
			if (Input.GetKeyDown (KeyCode.S)) {
				print ("Saving...");
				SaveAll ();
			}
		}

		public void SetCurrents(List<GameObject> gos) 
		{
			this.currents = gos;
		}

		

		public void SaveAll()
		{
			List <NeuralNet> nets = new List <NeuralNet>();
			
			storage.SetNeuralnets (nets);
			storage.Save (Path.Combine (Application.persistentDataPath, this.allFileName + ".xml"));
		}

		public List<NeuralNet> Load() {
			storage  = NetStorage.Load (Path.Combine(Application.persistentDataPath,this.loadFile + ".xml"));
			return storage.neuralNets;
		}

		public void AddToCurrents(GameObject go)
		{
			currents.Add (go);
		}
	}

}