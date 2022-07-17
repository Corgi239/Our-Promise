using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class GemSlotSnapController : MonoBehaviour
{
    [SerializeField] private List<GemSlot> _slots;
    [SerializeField] private List<Gem> _gems;
    [SerializeField] private float snapRange = 0.8f;

    public void Start()
    {
        _slots = FindObjectsOfType<GemSlot>().ToList();
        _gems = FindObjectsOfType<Gem>().ToList();
        foreach (Gem gem in _gems) {
            gem.dragStartedCallback = DetachFromSlot;
            gem.dragEndedCallback = SnapToNearestSlot;
        }
    }
    
    /// <summary>
    /// Detaches the gem from its GemSlot
    /// </summary>
    /// <param name="gem">Reference for the gem to be detached</param>
    private void DetachFromSlot(Gem gem)
    {
        if (gem.currentSlot == null) { return; }
        gem.currentSlot.Occupant = null;
        gem.currentSlot = null;
        gem.transform.parent = null;
        gem.transform.position = new Vector3(gem.transform.position.x, gem.transform.position.y, 0);
    }
    
    /// <summary>
    /// Searches for the nearest appropriate gem slot and attaches the gem to it if it is within snapping range
    /// </summary>
    /// <param name="gem">Reference to the gem for which the search is performed</param>
    private void SnapToNearestSlot(Gem gem)
    {
        float closestDistance = -1;
        GemSlot nearestSlot = null;

        foreach (GemSlot slot in _slots) {
            float currentDistance =
                Vector2.Distance(gem.transform.position, slot.transform.position);
            if (slot.Fits(gem) && (nearestSlot == null || currentDistance < closestDistance)) {
                nearestSlot = slot;
                closestDistance = currentDistance;
            }
        }

        if (nearestSlot != null && closestDistance <= snapRange) {
            nearestSlot.Occupant = gem;
            gem.currentSlot = nearestSlot;
            Transform gemTransform = gem.transform;
            gemTransform.parent = nearestSlot.transform;
            gemTransform.localPosition = new Vector3(0, 0, -1);
        }
    }
}
