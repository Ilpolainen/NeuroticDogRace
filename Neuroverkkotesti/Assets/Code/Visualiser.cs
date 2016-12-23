using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Code{
public class Visualiser : MonoBehaviour {

		World world;
		public GameObject thing;


		private Dictionary<IVisualizable, GameObject> objects;

		public Visualiser(){
			objects = new Dictionary<IVisualizable, GameObject>();
		}

		public void registerVisualizable(IVisualizable entry)
		{
			objects.Add(entry, entry.getVisualObject());
			entry.VisualEvent += OnVisualEvent;
		}
			

		protected virtual void OnVisualEvent(object o, EventArgs e)
		{
			//something happens in some visualisable
			Debug.Log("event");

		}

		// Use this for initialization
		void Start () {
			world = new World (this.gameObject, thing);
			registerVisualizable (world);
		}
		
		// Update is called once per frame
		void Update () {
			world.UpdatePosition ();
		}

		void FixedUpdate()
		{
			world.TimeStep ();
		}
	}
}