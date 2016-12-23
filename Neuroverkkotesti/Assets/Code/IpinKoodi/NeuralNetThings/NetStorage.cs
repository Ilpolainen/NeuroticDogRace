using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;
using System.Collections;

[XmlRoot("NNetStorage")]
public class NetStorage {


	[XmlArray("Neuralnets")]
	[XmlArrayItem("NeuralNet")]
	public List<NeuralNet> neuralNets;


	public NetStorage()
	{
		this.neuralNets = new List<NeuralNet> ();
	}

	public void Save(string path)
	{
		XmlSerializer serializer = new XmlSerializer (typeof(NetStorage));
		using (FileStream stream = new FileStream (path, FileMode.Create)) {
			serializer.Serialize (stream, this);
		}	
	}

	public static NetStorage Load(string path)
	{
		XmlSerializer serializer = new XmlSerializer (typeof(NetStorage),new XmlRootAttribute("NNetStorage") );
		using (FileStream stream = new FileStream (path, FileMode.Open)) {
			
			return serializer.Deserialize (stream) as NetStorage;
		}
	}

	public void SetNeuralnets(List<NeuralNet> nNets) 
	{
		this.neuralNets = nNets;
	}

	public NeuralNet GetNetWithIndex(int index) 
	{
		if (index >= neuralNets.Count || index < 0) {
			Debug.Log ("Index" + index + " Off Limits, no such NeuralNet!");
			return null;
		}
		return this.neuralNets [index];
	}

	public void SetNeuralNetWithIndex(int index, NeuralNet net)
	{
		if (index >= neuralNets.Count || index < 0) {
			Debug.Log ("Neuralstorage Off Limits! Cannot save with index " + index + "!");
			return;
		}
		this.neuralNets [index] = net;
	}
}
