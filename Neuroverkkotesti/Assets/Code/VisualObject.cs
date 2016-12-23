using System;
using UnityEngine;

namespace Code
{
	public class VisualObject : IVisualizable
	{
		public VisualObject(GameObject model){
			visualGo = GameObject.Instantiate(model);
			clearJoints ();
		}

		protected GameObject visualGo;

		protected Vector3 position;

		public event EventHandler VisualEvent;

		public GameObject getVisualObject ()
		{
			return visualGo;
		}
					


		public void UpdatePosition()
		{
			visualGo.transform.position = position;
		}

		private void clearPhysics(){
			Rigidbody[] rbs = visualGo.GetComponentsInChildren<Rigidbody> ();
			Collider[] colls = visualGo.GetComponentsInChildren<Collider> ();

			if (rbs != null) {
				foreach (Rigidbody rb in rbs) {
					GameObject.Destroy (rb);
				}
			}
			if (colls != null) {
				foreach (Collider col in colls) {
					GameObject.Destroy (col);
				}
			}
		}

		public void clearJoints() {
			Joint[] joints = visualGo.GetComponents<Joint> ();
			if (joints != null) {
				foreach (Joint j in joints) {
					GameObject.Destroy (j);
				}
			}
		}
	}
}

