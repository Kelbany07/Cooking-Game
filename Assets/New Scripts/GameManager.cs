using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ItemType heldItemType = ItemType.None;
    public GameObject heldItemPrefab; // optional visual

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool IsHoldingItem => heldItemType != ItemType.None;

    public void PickUpItem(ItemType type)
    {
        heldItemType = type;
        // Optionally instantiate a visual for the held item
    }

    public void ClearHeldItem()
    {
        heldItemType = ItemType.None;
        // Destroy held visual if you use one
    }
}
