using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class GemSlot : MonoBehaviour
{
   public GemSize slotSize;
   private Gem _occupant;
   public delegate void OccupantChangeCallback();
   public List<OccupantChangeCallback> occupantChangeCallbacks;
   [CanBeNull]
   public Gem Occupant
   {
      get => _occupant;
      set
      {
         _occupant = value;
         foreach (OccupantChangeCallback callback in occupantChangeCallbacks) {
            callback();
         }
      }
   }

   public void Initialize()
   {
      occupantChangeCallbacks = new List<OccupantChangeCallback>();
   }

   public bool IsVacant()
   {
      return Occupant == null;
   }
   public bool IsOccupied()
   {
      return !IsVacant();
   }
   public bool Fits(Gem gem)
   {
      return slotSize == gem.size && Occupant == null;
   }
   public override string ToString()
   {
      if (IsOccupied()) {
         return $"a {slotSize} slot with {Occupant.ToString()}";
      } else {
         return $"an empty {slotSize} slot";
      }
   }
}
