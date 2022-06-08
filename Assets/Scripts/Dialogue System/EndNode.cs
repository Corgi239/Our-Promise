using UnityEngine;

namespace Dialogue_System
{
    public class EndNode: BaseNode
    {
        [Input] public Connection entry;
        public override string GetDataString(Language lang, NarrativeState facts)
        {
            return "EndNode";
        }
    }
}