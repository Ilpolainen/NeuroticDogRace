using UnityEngine;
using System.Collections;

public static class ComponentUtilities  {

	// Use this for initialization

	
	// Update is called once per frame
	public static void HideObject(Renderer[] renderers) 
	{
		foreach (Renderer r in renderers) {
			r.enabled = false;
		}
	}

	public static void ShowObject(Renderer[] renderers)
	{
		foreach (Renderer r in renderers) {
			r.enabled = true;
		}
	}

	//public static Vector3 GetSteerablesBallCoords(Steerable steerable) {
		//Vector3 t = steerable.target.transform.position;
		//Vector3 vec = new Vector3 (t.x, t.y, t.z);
		//return vec;
	//}
}
