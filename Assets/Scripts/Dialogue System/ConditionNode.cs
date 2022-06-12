using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Dialogue_System
{
    public class ConditionNode: BaseNode
    {
        [Input] public Connection entry;
        [Output] public Connection pass;
        [Output] public Connection fail;
        public Condition[] conditions;

        public override string GetDataString(Language lang, NarrativeState facts)
        {
            bool res = true;
            foreach (Condition cond in conditions) {
                if (!cond.Evaluate(facts))
                    res = false;
            }
            return res ? "ConditionNode/Pass" : "ConditionNode/Fail";
        }
    }

    [Serializable]
    public struct Condition
    {
        public string fact;
        public Comparison comparison;
        public int value;

        public bool Evaluate(NarrativeState facts)
        {
            return comparison switch
            {
                Comparison.EQ => value == facts.ReadFact(fact),
                Comparison.NEQ => value != facts.ReadFact(fact),
                Comparison.GT => value < facts.ReadFact(fact),
                Comparison.LT => value > facts.ReadFact(fact),
                _ => false
            };
        }
    }
    
    public enum Comparison { EQ, NEQ, GT, LT, }
}

