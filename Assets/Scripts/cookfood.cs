using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookfood : MonoBehaviour
{
    public float cookingTime = 0;
    public int occupiedSlot = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (occupiedSlot == gameplay.SelectedSlot)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(objPosition.x, objPosition.y - .2f);
        }

        cookingTime += Time.deltaTime;
        if ((cookingTime > 5 && cookingTime < 10) && (transform.position.x >2))
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
}
