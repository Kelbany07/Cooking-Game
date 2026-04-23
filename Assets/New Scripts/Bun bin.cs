using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunBin : MonoBehaviour
{
    public ItemType bunType = ItemType.Bun;

    private void OnMouseDown()
    {
        if (!GameManager.Instance.IsHoldingItem)
        {
            GameManager.Instance.PickUpItem(bunType);
        }
    }
}

public class CounterSpot : MonoBehaviour
{
    public Transform placePoint;
    private GameObject currentItem;

    private void OnMouseDown()
    {
        if (!GameManager.Instance.IsHoldingItem) return;
        if (currentItem != null) return; // already occupied

        // Instantiate a bun (or other item) on the counter
        GameObject prefab = ItemDatabase.Instance.GetPrefab(GameManager.Instance.heldItemType);
        currentItem = Instantiate(prefab, placePoint.position, Quaternion.identity);
        GameManager.Instance.ClearHeldItem();
    }

    public GameObject TakeItem()
    {
        var temp = currentItem;
        currentItem = null;
        return temp;
    }
}
