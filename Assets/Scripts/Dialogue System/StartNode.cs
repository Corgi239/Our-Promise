using UnityEngine;

namespace Dialogue_System
{
    public class StartNode: BaseNode
    {
        [Output] public int exit;
        public override string GetDataString()
        {
            return "StartNode";
        }
    }
}