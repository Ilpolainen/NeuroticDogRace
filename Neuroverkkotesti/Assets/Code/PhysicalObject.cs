using System;
using UnityEngine;

namespace Code
{
	public class PhysicalObject : VisualObject
	{
		private GameObject physGo;
		private Rigidbody rb;
	

		public PhysicalObject (GameObject model) : base(model)
		{
			physGo = GameObject.Instantiate (model);
			clearPhysGoRenderers ();
			rb = physGo.GetComponent<Rigidbody> ();
		}

		public GameObject getPhysicalGo()
		{
			return physGo;
		}

		public void Update(){
			this.position = physGo.transform.position;
			this.UpdatePosition ();
		}



		private void clearPhysGoRenderers() 
		{
			MeshFilter[] filters = physGo.GetComponentsInChildren<MeshFilter> ();
			Renderer[] renderers = physGo.GetComponentsInChildren<Renderer> ();
			foreach (Renderer r in renderers) {
				GameObject.Destroy (r);
			}
			foreach (MeshFilter mf in filters) {
				GameObject.Destroy (mf);
			}
		}
	}
}

