using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Simple version: if holding any item, throw it away
        if (GameManager.Instance.IsHoldingItem)
        {
            // If you want only burnt items:
            // if (GameManager.Instance.heldItemType == ItemType.PattyBurnt) { ... }
            GameManager.Instance.ClearHeldItem();
        }
    }
}
