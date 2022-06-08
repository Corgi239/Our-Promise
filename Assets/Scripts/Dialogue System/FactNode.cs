using System.Linq;

namespace Dialogue_System
{
    public class FactNode: BaseNode
    {
        [Input] public Connection entry;
        [Output] public Connection exit;
        public NarrativeState.FactsDictionary changes;
        
        public override string GetDataString(Language lang, NarrativeState facts)
        {
            return changes.Aggregate("FactNode", (current, fact) => current + $"/{fact.Key}:{fact.Value}");
        }
    }
}