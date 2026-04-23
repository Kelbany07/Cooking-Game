using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movesandwich : MonoBehaviour
{
    public int occupiedSlot = 0;
    public string mousecontrolled = "n";

    void Start()
    {
        // keep this as a fallback, but prefer explicit assignment at Instantiate time
        if (occupiedSlot == 0)
            occupiedSlot = gameplay.SelectedSlot;
        Debug.Log("movesandwich.Start slot=" + occupiedSlot);
    }

    void Update()
    {
        // match other scripts: become mouse-controlled when the global selected sandwich matches
        if (occupiedSlot == gameplay.selectedSandwhich)
        {
            mousecontrolled = "y";
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }

        if ((gameplay.deleteFood == "y") && (mousecontrolled == "y"))
        {
            gameplay.deleteFood = "n";
            gameplay.selectedSandwhich = 0;
            mousecontrolled = "n";
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // set the global selection so all parts with the same occupiedSlot respond
        gameplay.selectedSandwhich = occupiedSlot;
        mousecontrolled = "y";
        Debug.Log("movesandwich.OnMouseDown selectedSandwhich=" + gameplay.selectedSandwhich + " occupiedSlot=" + occupiedSlot);
    }

    private void OnMouseUp()
    {
        mousecontrolled = "n";
        gameplay.selectedSandwhich = 0;
    }
}
