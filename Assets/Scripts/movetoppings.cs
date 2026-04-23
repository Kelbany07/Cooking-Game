using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetoppings : MonoBehaviour
{
    public int occupiedSlot = 99;
    public string mousecontrolled = "n";
    // Start is called before the first frame update
    void Start()
    {
        occupiedSlot = gameplay.SelectedSlot;
    }

    // Update is called once per frame
    void Update()
    {
        if (occupiedSlot == gameplay.selectedSandwhich)
        {
            mousecontrolled = "y";
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(objPosition.x, objPosition.y - .4f);
        }

        if ((gameplay.deleteFood == "y") && (mousecontrolled == "y"))
        {
            // clear global delete request and selection so it won't delete every later object
            gameplay.deleteFood = "n";
            gameplay.selectedSandwhich = 0;
            mousecontrolled = "n";
            Destroy(gameObject);
            return;
        }
    }
}
