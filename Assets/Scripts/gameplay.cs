using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameplay : MonoBehaviour
{

    public static string cuttingboardS1="empty";
    public static string cuttingboardS2="empty";
    public static string cuttingboardS3="empty";

    public static string grillS1 = "empty";
    public static string grillS2 = "empty";
    public static string grillS3 = "empty";

    public static int SelectedSlot=0;
    public static int selectedSandwhich = 0;

    public KeyCode giveFood;
    public static string deleteFood= "n";
    public static string currentMeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(giveFood))
        {
            deleteFood = "y";
        }
    }

    // Remove all GameObjects that belong to a sandwich slot (buns, patty, toppings, assembled sandwich)
    public static void RemoveSandwich(int slot)
    {
        if (slot <= 0)
            return;

        // Destroy any movesandwich objects for this slot
        var sandwiches = Object.FindObjectsOfType<movesandwich>();
        foreach (var s in sandwiches)
        {
            if (s != null && s.occupiedSlot == slot)
            {
                Object.Destroy(s.gameObject);
            }
        }

        // Destroy any movetoppings objects for this slot
        var toppings = Object.FindObjectsOfType<movetoppings>();
        foreach (var t in toppings)
        {
            if (t != null && t.occupiedSlot == slot)
            {
                Object.Destroy(t.gameObject);
            }
        }

        // Destroy any cookfood objects for this slot (patty, etc.)
        var foods = Object.FindObjectsOfType<cookfood>();
        foreach (var f in foods)
        {
            if (f != null && f.occupiedSlot == slot)
            {
                Object.Destroy(f.gameObject);
            }
        }

        // clear the selected sandwich slot
        selectedSandwhich = 0;
    }
}
