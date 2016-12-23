using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Code;
using System.Collections.Generic;
using System.Collections;
using System;

[TestFixture]
public class NNetTest {

	[Test]
	public void NNetTestClass()
	{
		List<int> s = new List<int>();
		s.Add (3);
		s.Add (2);
		s.Add (1);
		List<float> input = new List<float>();
		input.Add (1f);
		input.Add (-1f);
		input.Add (0f);
		MockNet net = new MockNet(s);
		IList<float> output = net.Output (input);
		Assert.AreEqual(output.Count, 1);
		Assert.True (output [0] <= 1 && output [0] >= -1);
	}
}
