using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class GemSlotSnapController : MonoBehaviour
{
    private List<GemSlot> _slots;
    private List<Gem> _gems;
    [SerializeField] private float snapRange = 0.8f;

    public void Start()
    {
        _slots = FindObjectsOfType<GemSlot>().ToList();
        _gems = FindObjectsOfType<Gem>().ToList();
        foreach (Gem gem in _gems) {
            gem.dragEndedCallback = OnDragEnded;
        }
    }

    /// <summary>
    /// Searches for the nearest appropriate gem slot and attaches the gem to it if it is within snapping range
    /// </summary>
    /// <param name="gem">Reference to the gem for which the search is performed</param>
    private void OnDragEnded(Gem gem)
    {
        float closestDistance = -1;
        GemSlot closestSlot = null;

        foreach (GemSlot slot in _slots) {
            float currentDistance =
                Vector2.Distance(gem.transform.position, slot.transform.position);
            if (slot.Fits(gem) && (closestSlot == null || currentDistance < closestDistance)) {
                closestSlot = slot;
                closestDistance = currentDistance;
            }
        }

        if (closestSlot != null && closestDistance <= snapRange) {
            Transform gemTransform = gem.transform;
            gemTransform.parent = closestSlot.transform;
            gemTransform.localPosition = Vector2.zero;
        }
    }
}
