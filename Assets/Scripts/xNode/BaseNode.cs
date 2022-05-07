using UnityEngine;
using XNode;

namespace xNode
{
	public abstract class BaseNode : Node
	{
		public abstract string GetString();

		public abstract Sprite GetSprite();
	}
}