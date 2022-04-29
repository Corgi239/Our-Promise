using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem Textual Data", menuName = "ScriptableObjects/Gem Textual Data", order = 1)]
public class GemTextualData : ScriptableObject
{
    [SerializeField] public GemTextualRecord[] gemTextualRecords;
    [SerializeField] public ResonanceEffectTextualRecord[] resonanceTextualRecords;
    
    public Dictionary<(GemType, GemSize, GemCut), Sprite> Sprite;
    public Dictionary<GemType, string> BodyEffect;
    public Dictionary<GemType, string> ReflexEffect;
    public Dictionary<GemType, string> MindEffect;
    public Dictionary<GemType, string> CodexEntry;
    public Dictionary<HashSet<GemType>, string> Virtue;
    public Dictionary<HashSet<GemType>, string> Vice;
    

    public void Initialize()
    {
        BodyEffect = new Dictionary<GemType, string>();
        ReflexEffect = new Dictionary<GemType, string>();
        MindEffect = new Dictionary<GemType, string>();
        CodexEntry = new Dictionary<GemType, string>();
        foreach (GemTextualRecord record in gemTextualRecords) {
            BodyEffect[record.type] = record.bodyEffect;
            ReflexEffect[record.type] = record.reflexEffect;
            MindEffect[record.type] = record.mindEffect;
            CodexEntry[record.type] = record.codexEntry;
        }

        Virtue = new Dictionary<HashSet<GemType>, string>(HashSet<GemType>.CreateSetComparer());
        Vice = new Dictionary<HashSet<GemType>, string>(HashSet<GemType>.CreateSetComparer());
        foreach (ResonanceEffectTextualRecord record in resonanceTextualRecords) {
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
public class GemTextualRecord
{
    public GemType type;
    public string bodyEffect;
    public string reflexEffect;
    public string mindEffect;
    public string codexEntry;
}

[Serializable]
public class ResonanceEffectTextualRecord
{
    public GemType firstType;
    public GemType secondType;
    public string virtue;
    public string vice;
}
