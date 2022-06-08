using UnityEngine;

namespace Dialogue_System
{
    public class StartNode: BaseNode
    {
        [Output] public Connection exit;
        public override string GetDataString(Language lang, NarrativeState facts)
        {
            return "StartNode";
        }
    }
}