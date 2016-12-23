using UnityEngine;
using System.Collections.Generic;

namespace Code
{
	public interface INeuralNet {

		IList<float> Output(IList<float> input);
	}
}
