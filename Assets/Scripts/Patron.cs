using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    public string orderedMeat = "hamburger";
    public bool served = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called by a food object when dropped onto this Patron
    public void ReceiveFood(string foodType, GameObject foodObject)
    {
        if (served)
        {
            Debug.Log(gameObject.name + " already served.");
            return;
        }

        if (string.IsNullOrEmpty(foodType))
        {
            Debug.Log(gameObject.name + " received nothing.");
            return;
        }

        if (foodType == orderedMeat)
        {
            Debug.Log(gameObject.name + " received correct order: " + foodType);
            // TODO: award points, play animation, update UI
        }
        else
        {
            Debug.Log(gameObject.name + " received wrong order. Wanted: " + orderedMeat + " got: " + foodType);
            // TODO: negative feedback
        }

        // Remove all parts for the currently selected sandwich slot
        gameplay.RemoveSandwich(gameplay.selectedSandwhich);

        // As a fallback, destroy the specific food object if it still exists
        if (foodObject != null)
        {
            Destroy(foodObject);
        }

        served = true;

        // Optionally clear or change the order:
        orderedMeat = "none";
    }
}
