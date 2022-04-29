using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameData.GameDataScripts;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    [SerializeField] protected GemData gemData;
    [SerializeField] private GemSlot[] slots;
    [SerializeField] private ArtifactMaterial material;
    [SerializeField] private ResonantPair[] resonantPairs;

    public void Awake()
    {
        int l = slots.Length;
        List<ResonantPair> pairs = new List<ResonantPair>();
        for (int i = 0; i < l-1; i++) {
            for (int j = i + 1; j < l; j++) {
                pairs.Add(new ResonantPair(slots[i], slots[j]));
            }
        }
        pairs.First().SetGemData(this.gemData);
        this.resonantPairs = pairs.ToArray();
    }

    private Gem[] Gems()
    {
        return this.slots.Select(s => s.occupant).OfType<Gem>().ToArray(); 
    }
    
    private Gem LargestGem()
    {
        if (Array.Exists(Gems(), g => g.size == GemSize.Large)) { 
            return Array.Find(Gems(), g => g.size == GemSize.Large);
        } else if (Array.Exists(Gems(), g => g.size == GemSize.Medium)) {
            return Array.Find(Gems(), g => g.size == GemSize.Medium);
        } else if (Array.Exists(Gems(), g => g.size == GemSize.Small)) {
            return Array.Find(Gems(), g => g.size == GemSize.Small);
        } else {
            return null;
        }
    }
    
    private string TextualDescription()
    {
        string artifactDescription = $"An artifact made of {material}\n";
        string structureSummary = "Structure\n";
        for (int i = 0; i < slots.Length; i++) {
            string line = (i != slots.Length - 1) ? $"├ {slots[i]}" : $"└ {slots[i]}";
            structureSummary += line + "\n";
        }
        string effectsSummary = "Effects\n";
        Gem largestGem = LargestGem();
        string mainEffect = largestGem != null ? largestGem.EffectDescription(this.material) + $" ({largestGem.size})\n" : "Empty\n";
        effectsSummary += "├ Main effect: " + mainEffect;
        string[] sideeffects = resonantPairs.Select(p => p.ToString()).ToArray();
        for (int i = 0; i < sideeffects.Length; i++) {
            string line = (i != sideeffects.Length - 1) ? $"├ Side effect: {sideeffects[i]}" : $"└ Side effect: {sideeffects[i]}";
            effectsSummary += line + "\n";
        }

        return artifactDescription + "\n" + structureSummary + "\n" + effectsSummary;
    }

    public void OnMouseDown()
    {
        Debug.Log(this.TextualDescription());
    }
}
public enum ArtifactMaterial{Iron, Wood, Bone}

[Serializable]
public class ResonantPair
{
    [SerializeField] private GemSlot slot1;
    [SerializeField] private GemSlot slot2;
    private static GemData _gemData;
    
    public ResonantPair(GemSlot firstSlot, GemSlot secondSlot)
    {
        this.slot1 = firstSlot;
        this.slot2 = secondSlot;
    }

    public void SetGemData(GemData gemData) { _gemData = gemData; }

    public bool IsInResonance()
    {
        //TODO: Implement once we have figured out gem cuts
        return slot1.IsOccupied() && slot2.IsOccupied() && (slot1.occupant.type != slot2.occupant.type);
    }

    public override string ToString()
    {
        if (IsInResonance()) {
            var set = new HashSet<GemType> {slot1.occupant.type, slot2.occupant.type};
            return _gemData.Virtue[set];
        } else {
            return $"{slot1} and {slot2} out of resonance";
        }
    }
}
