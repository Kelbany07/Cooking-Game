using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    public Transform grillPoint;
    public float cookTime = 5f;
    public float burnTime = 10f;

    private PattyOnGrill currentPatty;

    private void OnMouseDown()
    {
        // Place raw patty on grill
        if (currentPatty == null && GameManager.Instance.heldItemType == ItemType.PattyRaw)
        {
            GameObject prefab = ItemDatabase.Instance.GetPrefab(ItemType.PattyRaw);
            GameObject pattyObj = Instantiate(prefab, grillPoint.position, Quaternion.identity);
            currentPatty = pattyObj.GetComponent<PattyOnGrill>();
            currentPatty.StartCooking(cookTime, burnTime);
            GameManager.Instance.ClearHeldItem();
        }
        // Pick up cooked patty (only if cooked, not burnt)
        else if (currentPatty != null && currentPatty.State == PattyState.Cooked && !GameManager.Instance.IsHoldingItem)
        {
            GameManager.Instance.PickUpItem(ItemType.PattyCooked);
            Destroy(currentPatty.gameObject);
            currentPatty = null;
        }
    }
}

public class PattyOnGrill : MonoBehaviour
{
    public PattyState State { get; private set; } = PattyState.Raw;
    public Sprite rawSprite;
    public Sprite cookedSprite;
    public Sprite burntSprite;

    private float cookTime;
    private float burnTime;
    private float timer;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = rawSprite;
    }

    public void StartCooking(float cookTime, float burnTime)
    {
        this.cookTime = cookTime;
        this.burnTime = burnTime;
        State = PattyState.Cooking;
        timer = 0f;
    }

    private void Update()
    {
        if (State == PattyState.Cooking || State == PattyState.Cooked)
        {
            timer += Time.deltaTime;

            if (State == PattyState.Cooking && timer >= cookTime)
            {
                State = PattyState.Cooked;
                sr.sprite = cookedSprite;
            }

            if (timer >= burnTime)
            {
                State = PattyState.Burnt;
                sr.sprite = burntSprite;
            }
        }
    }
}
