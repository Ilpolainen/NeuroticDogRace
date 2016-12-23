using UnityEngine;
using System.Collections;

namespace Code {

	public class God : MonoBehaviour {

		private GameObject[] units;
		private Creator creator;
		// Use this for initialization
		void Start () {
			creator = this.gameObject.GetComponent<Creator> ();
			units = creator.CreateEmptyObjects (50);
			creator.AttachHorses (units);
			creator.AttachMinds (units);
		}
	
		// Update is called once per frame
		void Update () {
			
		}

		public GameObject[] GetUnits() 
		{
			return units;
		}
	}
}