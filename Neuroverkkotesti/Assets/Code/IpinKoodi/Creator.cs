using UnityEngine;
using System.Collections;

namespace Code {

	public class Creator : MonoBehaviour {

		public GameObject model;


	// Use this for initialization
		void Start () {
		
		}

	// Update is called once per frame
		void Update () {
	
		}



		public GameObject[] CreateEmptyObjects(int size) {
			GameObject[] list = new GameObject[size];
			for (int i = 0; i < size/5; i++) {
				for (int j = 0; j < 5; j++) {
					if (i * 5 + j < size) {
						Vector3 position = new Vector3 (j * 10, 0, -6 * i);
						GameObject thing = new GameObject ();
						thing.transform.position = position;
						list [i * 5 + j] = thing;
					}
				}
			}
			return list;
		}

		public void AttachHorses(GameObject[] list) 
		{
			for (int i = 0; i < list.Length; i++) {
				list [i].AddComponent<Steerable>();
				list [i].GetComponent<Steerable> ().physGo = GameObject.Instantiate (model,list[i].transform.position,Quaternion.identity) as GameObject;
			}
		}

		public void AttachMinds(GameObject[] list)
		{
			for (int i = 0; i < list.Length; i++) {
				list [i].AddComponent<Mind>();
			}
		}
	}
}