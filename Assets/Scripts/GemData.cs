using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem Data", menuName = "ScriptableObjects/Gem Data", order = 1)]
public class GemData : ScriptableObject
{
    [Tooltip("Information about gems")] [SerializeField]
    private GemRecord[] gemDetails;
    [Tooltip("Information about effects of gem resonance of form (Type 1, Type 2, Virtue effect description, Vice effect description)")] [SerializeField]
    private ResonanceEffectRecord[] resonanceEffects;

    public Dictionary<GemType, Color> Color;
    public Dictionary<GemType, string> BodyEffect;
    public Dictionary<GemType, string> ReflexEffect;
    public Dictionary<GemType, string> MindEffect;
    public Dictionary<HashSet<GemType>, string> Virtue;
    public Dictionary<HashSet<GemType>, string> Vice;

    public void OnEnable()
    {
        Color = new Dictionary<GemType, Color>();
        BodyEffect = new Dictionary<GemType, string>();
        ReflexEffect = new Dictionary<GemType, string>();
        MindEffect = new Dictionary<GemType, string>();
        foreach (GemRecord record in gemDetails) {
            Color[record.type] = record.color;
            BodyEffect[record.type] = record.bodyEffect;
            ReflexEffect[record.type] = record.reflexEffect;
            MindEffect[record.type] = record.mindEffect;
        }

        Virtue = new Dictionary<HashSet<GemType>, string>(HashSet<GemType>.CreateSetComparer());
        Vice = new Dictionary<HashSet<GemType>, string>(HashSet<GemType>.CreateSetComparer());
        foreach (ResonanceEffectRecord record in resonanceEffects) {
            var set = new HashSet<GemType>
            {
                record.firstType,
                record.secondType
            };
            Virtue[set] = record.virtue;
            Vice[set] = record.vice;
        }
    }
}

[Serializable]
public class GemRecord
{
    public GemType type;
    public Color color;
    public string bodyEffect;
    public string reflexEffect;
    public string mindEffect;
    public string codexEntry;
}

[Serializable]
public class ResonanceEffectRecord
{
    public GemType firstType;
    public GemType secondType;
    public string virtue;
    public string vice;
}
