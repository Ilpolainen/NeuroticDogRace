using System;
using UnityEngine;
using System.Collections.Generic;


namespace Code
{
	public class World : IVisualizable
	{
		private GameObject visualGo;
		private GameObject physicsGo;
		private List<PhysicalObject> children;


		public World (GameObject gameObject, GameObject model)
		{
			visualGo = gameObject;
			physicsGo = new GameObject ();
			//physicsGo.transform.SetParent (visualGo.transform);

			children = new List<PhysicalObject> ();
//			thing = new Steerable (model);
//			AddChild (thing);
		}

		public UnityEngine.GameObject getVisualObject ()
		{
			return visualGo;
		}

		int tick = 0;

		public event EventHandler VisualEvent;
		private Steerable thing;


		private void AddChild(PhysicalObject newChild)
		{
			children.Add (newChild);
			newChild.getVisualObject ().transform.parent = visualGo.transform;
			newChild.getPhysicalGo ().transform.parent = physicsGo.transform;
		}

		public void TimeStep()
		{
			
			tick++;
			if (tick % 200 == 0) {
				OnVisualEvent (new EventArgs ());
			}
		}

		protected virtual void OnVisualEvent(EventArgs e)
		{
			EventHandler handler = VisualEvent;
			if (handler != null) {
				handler (this, e);
			}
		}

		public void UpdatePosition ()
		{
			foreach (PhysicalObject obj in children) {				
				obj.Update ();
			}
		}
	}
}

