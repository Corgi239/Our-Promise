using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogue_System
{
    [CreateAssetMenu(fileName = "New Narrative State", menuName = "ScriptableObjects/Narrative State", order = 1)]
    public class NarrativeState : ScriptableObject
    {
        [Serializable]
        public class FactsDictionary : SerializableDictionary<string, int> { }
        [SerializeField] private FactsDictionary facts;

        

    }
}

