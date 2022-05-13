using System;
using UnityEngine;
using XNode;

namespace Dialogue_System
{
	public class BaseNode : Node
	{
		public virtual string GetDataString(Language lang)
		{
			return null;
		}

		public virtual Sprite GetSprite()
		{
			return null;
		}

		// This is here to get rid of a warning.
		// I am misusing the xNode library somewhat, which is why I have to do this sort of stuff
		public override object GetValue(NodePort port)
		{
			return null;
		}
	}

	public enum Language {ENG, RUS}
	
	[Serializable]
	public struct Connection { }
}