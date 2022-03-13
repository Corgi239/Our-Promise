using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class GemSlot : MonoBehaviour
{
   public GemSize slotSize;
   [CanBeNull] public Gem occupant;

   public bool IsVacant()
   {
      return occupant == null;
   }

   public bool IsOccupied()
   {
      return !IsVacant();
   }
   public bool Fits(Gem gem)
   {
      return slotSize == gem.size && occupant == null;
   }

   public override string ToString()
   {
      if (IsOccupied()) {
         return $"a {slotSize} slot with {occupant.ToString()}";
      } else {
         return $"an empty {slotSize} slot";
      }
   }
}
