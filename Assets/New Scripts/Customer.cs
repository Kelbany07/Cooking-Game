using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem = ItemType.BurgerComplete;
    public Slider patienceSlider;
    public float maxPatience = 20f;

    private float currentPatience;
    private bool hasOrder = true;
    private CustomerSpawner spawner;

    public void Init(CustomerSpawner spawnerRef)
    {
        spawner = spawnerRef;
        currentPatience = maxPatience;
        patienceSlider.maxValue = maxPatience;
        patienceSlider.value = maxPatience;
        hasOrder = true;
    }

    private void Update()
    {
        if (!hasOrder) return;

        currentPatience -= Time.deltaTime;
        patienceSlider.value = currentPatience;

        if (currentPatience <= 0f)
        {
            Leave(false);
        }
    }

    private void OnMouseDown()
    {
        // Player gives item to customer
        if (!GameManager.Instance.IsHoldingItem) return;
        if (!hasOrder) return;

        if (GameManager.Instance.heldItemType == desiredItem)
        {
            GameManager.Instance.ClearHeldItem();
            hasOrder = false;
            Leave(true);
        }
        else
        {
            // Wrong item: you can penalize or ignore
        }
    }

    private void Leave(bool served)
    {
        // Notify spawner that this spot is free
        if (spawner != null)
        {
            spawner.CustomerLeft(this, served);
        }
        Destroy(gameObject);
    }
}
