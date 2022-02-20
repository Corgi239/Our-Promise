using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class GemSlot : MonoBehaviour
{
   public GemSize slotSize;
   [CanBeNull] public Gem occupant;
   public bool Fits(Gem gem)
   {
      return slotSize == gem.size && occupant == null;
   }
}
