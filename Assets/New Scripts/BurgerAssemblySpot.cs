using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerAssemblySpot : MonoBehaviour
{
    public Transform placePoint;
    private bool hasBottomBun = false;
    private bool hasPatty = false;
    private GameObject bunObj;
    private GameObject pattyObj;

    private void OnMouseDown()
    {
        if (!GameManager.Instance.IsHoldingItem) return;

        if (!hasBottomBun && GameManager.Instance.heldItemType == ItemType.Bun)
        {
            bunObj = Instantiate(ItemDatabase.Instance.GetPrefab(ItemType.Bun), placePoint.position, Quaternion.identity);
            hasBottomBun = true;
            GameManager.Instance.ClearHeldItem();
        }
        else if (hasBottomBun && !hasPatty && GameManager.Instance.heldItemType == ItemType.PattyCooked)
        {
            pattyObj = Instantiate(ItemDatabase.Instance.GetPrefab(ItemType.PattyCooked), placePoint.position + Vector3.up * 0.2f, Quaternion.identity);
            hasPatty = true;
            GameManager.Instance.ClearHeldItem();
        }
        else if (hasBottomBun && hasPatty && GameManager.Instance.heldItemType == ItemType.Bun)
        {
            // Complete burger
            Destroy(bunObj);
            Destroy(pattyObj);
            GameObject burger = Instantiate(ItemDatabase.Instance.GetPrefab(ItemType.BurgerComplete), placePoint.position, Quaternion.identity);
            // Immediately pick up the burger
            GameManager.Instance.PickUpItem(ItemType.BurgerComplete);
            Destroy(burger, 0.01f); // just using prefab as visual; you can keep it instead
            ResetAssembly();
        }
    }

    private void ResetAssembly()
    {
        hasBottomBun = false;
        hasPatty = false;
        bunObj = null;
        pattyObj = null;
    }
}
