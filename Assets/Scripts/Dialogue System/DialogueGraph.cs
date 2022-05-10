using System;
using UnityEngine;
using XNode;

namespace Dialogue_System
{
	[CreateAssetMenu]
	public class DialogueGraph : NodeGraph
	{
		public BaseNode current;

		public BaseNode GetRootNode()
		{
			foreach (Node node in nodes) {
				if (node is StartNode) return node as BaseNode;
			}

			throw new Exception("No Start Node found in dialogue graph");
		}
	}
}