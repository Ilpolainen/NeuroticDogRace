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
		
}
