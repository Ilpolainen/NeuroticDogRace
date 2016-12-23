using System;
using UnityEngine;

namespace Code
{
	public interface IVisualizable
	{
		event EventHandler VisualEvent;	
		GameObject getVisualObject();
		void UpdatePosition();
	}

}

