using UnityEngine;
using System.Collections;
namespace Code {
public class Chooser : MonoBehaviour {

		private int pointer;
		private Steerable[] objects;
		public God god;
		public FileManager fileManager;
	// Use this for initialization
		void Start () {
			pointer = 0;
		}
	
		// Update is called once per frame
		void Update () {
			if (Time.frameCount == 10) {
				SetObjects ();
			} else {
				if (Input.GetKey (KeyCode.RightArrow)) {
					SetCurrent (0);
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
					SetCurrent (1);
				}
				if (Input.GetKeyDown (KeyCode.A)) {
					AddToFilemanager ();
				}
			}
		}

		public GameObject GetCurrent()
		{
			return objects [pointer].physGo;
		}

		public void SetObjects()
		{
			GameObject[] emptyObs = god.GetUnits ();
			objects = new Steerable[emptyObs.Length];
			for (int i = 0; i < emptyObs.Length; i++) {
				objects [i] = emptyObs [i].GetComponent<Steerable> ();
			}
		}

		public void SetCurrent(int direction) 
		{
			if (NotInActives ()) {
				objects [pointer].physGo.GetComponentInChildren<Renderer> ().material.color = Color.white;
			} else {
				objects [pointer].physGo.GetComponentInChildren<Renderer> ().material.color = Color.red;
			}
			if (direction == 0) {
				pointer = (pointer + 1) % objects.Length;
			}
			if (direction == 1) {
				pointer = (pointer + objects.Length - 1) % objects.Length;
			}
			if (NotInActives()) {
				objects [pointer].physGo.GetComponentInChildren<Renderer> ().material.color = Color.green;
			} else {
				objects [pointer].physGo.GetComponentInChildren<Renderer> ().material.color = Color.cyan;
			}
		}

		private void AddToFilemanager() {
			objects[pointer].physGo.GetComponentInChildren<Renderer> ().material.color = Color.red;
			fileManager.AddToCurrents (objects[pointer].gameObject);
		}

		private bool NotInActives() {
			if (objects[pointer].physGo.GetComponentInChildren<Renderer> ().material.color == Color.red) {
				return false;
			}
			if (objects[pointer].physGo.GetComponentInChildren<Renderer> ().material.color == Color.cyan) {
				return false;
			}
			return true;
		}
	}
}