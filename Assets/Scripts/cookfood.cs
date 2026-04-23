using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookfood : MonoBehaviour
{
    public float cookingTime = 0;
    public int occupiedSlot = 100;
    public string mousecontrolled = "n";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (occupiedSlot == gameplay.selectedSandwhich)
        {
            mousecontrolled = "y";
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(objPosition.x, objPosition.y - .2f);
            if (gameObject.name == "burger patty(Clone)")
            {
                gameplay.currentMeat = "hamburger";
            }
        }

        // If the player clicked the trash bin while holding this, destroy and clear the delete flag
        if ((gameplay.deleteFood == "y") && (mousecontrolled == "y"))
        {
            // clear the global delete request and selection so it doesn't affect other objects
            gameplay.deleteFood = "n";
            gameplay.selectedSandwhich = 0;
            mousecontrolled = "n";
            Destroy(gameObject);
            return;
        }

        cookingTime += Time.deltaTime;
        if ((cookingTime > 5 && cookingTime < 10) && (transform.position.x > 2))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
        }
        if ((cookingTime > 10) && (transform.position.x > 2))
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
        if (occupiedSlot == gameplay.selectedSandwhich)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }

    void OnMouseDown()
    {
        if (gameplay.cuttingboardS1 == "justbun")
        {
            if (transform.position.x == 5)
            {
                gameplay.grillS1 = "empty";
            }
            if (transform.position.x == 7)
            {
                gameplay.grillS2 = "empty";
            }
            if (transform.position.x == 7)
            {
                gameplay.grillS3 = "empty";
            }
            GetComponent<Transform>().position = new Vector2(-1, -0.8f);
            gameplay.cuttingboardS1 = "fullbun";
            occupiedSlot = 1;
        }
        else
            if (gameplay.cuttingboardS2 == "justbun")
        {
            GetComponent<Transform>().position = new Vector2(1, -0.8f);
            gameplay.cuttingboardS2 = "fullbun";
            occupiedSlot = 2;
        }
        else
            if (gameplay.cuttingboardS3 == "justbun")
        {
            GetComponent<Transform>().position = new Vector2(0, -2f);
            gameplay.cuttingboardS3 = "fullbun";
            occupiedSlot = 3;
        }
    }

    void OnMouseUp()
    {
        if (mousecontrolled != "y")
            return;

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check for a 2D collider at the cursor position
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld);
        if (hit != null)
        {
            // Accept either a specific GameObject name or a "Customer" tag
            if (hit.gameObject.name == "Customer_ProtoSprite" || hit.CompareTag("Customer"))
            {
                Patron patron = hit.GetComponent<Patron>();
                if (patron != null)
                {
                    // Pass the meat type and this food GameObject to the Patron
                    patron.ReceiveFood(gameplay.currentMeat, gameObject);

                    // Clear selection / state on successful handoff
                    gameplay.currentMeat = "";
                    gameplay.selectedSandwhich = 0;
                    mousecontrolled = "n";
                    occupiedSlot = 100;
                    return;
                }
            }
        }

        // If not given to a customer, leave the food where released (or add fallback behavior)
        mousecontrolled = "n";
        occupiedSlot = 100;
    }
}
