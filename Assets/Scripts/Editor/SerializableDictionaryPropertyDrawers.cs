using Dialogue_System;
using UnityEditor;

namespace Editor
{
    [CustomPropertyDrawer(typeof(NarrativeState.FactsDictionary))]
    public class FactsDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}

