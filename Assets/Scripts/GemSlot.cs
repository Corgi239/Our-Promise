using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSlot : MonoBehaviour
{
   public GemSize slotSize;
   
   public bool Fits(Gem gem)
   {
      return slotSize == gem.size;
   }
}
