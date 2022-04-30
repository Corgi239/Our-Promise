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
    private ResonantPair[] resonantPairs;

    public void Awake()
    {
        foreach(GemSlot slot in slots) {slot.Initialize();}
        int l = slots.Length;
        List<ResonantPair> pairs = new List<ResonantPair>();
        for (int i = 0; i < l-1; i++) {
            for (int j = i + 1; j < l; j++) {
                ResonantPair newPair = new ResonantPair(slots[i], slots[j]);
                newPair.SetGemData(gemData);
                pairs.Add(newPair);
                newPair.UpdateConnection();
            }
        }
        resonantPairs = pairs.ToArray();
    }

    private Gem[] Gems()
    {
        return slots.Select(s => s.Occupant).OfType<Gem>().ToArray(); 
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
    private LineRenderer connectionRenderer;
    private static GemData _gemData;
    
    public ResonantPair(GemSlot firstSlot, GemSlot secondSlot)
    {
        this.slot1 = firstSlot;
        this.slot2 = secondSlot;
        InitializeCallbacks();
    }

    private void InitializeCallbacks()
    {
        slot1.occupantChangeCallbacks.Add(this.UpdateConnection);
        slot2.occupantChangeCallbacks.Add(this.UpdateConnection);
    }

    private void CreateConnection()
    {
        GameObject connectionObject = new GameObject("Connection", typeof(LineRenderer));
        LineRenderer renderer = connectionObject.GetComponent<LineRenderer>();
        renderer.startWidth = 0.05f;
        renderer.material = _gemData.resonanceConnectionMaterial;
        renderer.useWorldSpace = false;
        renderer.transform.parent = slot1.transform;
        renderer.positionCount = 2;
        renderer.SetPosition(0, slot1.transform.position);
        renderer.SetPosition(1, slot2.transform.position);
        this.connectionRenderer = renderer;
    }

    public void UpdateConnection()
    {
        if (connectionRenderer == null) {
            CreateConnection();
        }
        connectionRenderer.enabled = IsInResonance();
    }

    public void SetGemData(GemData gemData) { _gemData = gemData; }

    public bool IsInResonance()
    {
        return slot1.IsOccupied() && slot2.IsOccupied() && 
               (slot1.Occupant.type != slot2.Occupant.type) && 
               (slot1.Occupant.cut == slot2.Occupant.cut);
    }

    public override string ToString()
    {
        if (IsInResonance()) {
            var set = new HashSet<GemType> {slot1.Occupant.type, slot2.Occupant.type};
            return _gemData.Virtue[set];
        } else {
            return $"[{slot1}] and [{slot2}] out of resonance";
        }
    }
}
