using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookfood : MonoBehaviour
{
    public float cookingTime = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        cookingTime += Time.deltaTime;
        if ((cookingTime > 5 && cookingTime < 10) && (transform.position.x >2))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
        }
        if ((cookingTime > 10) && (transform.position.x > 2))
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
    }
    private void OnMouseDown()
    {
        if (gameplay.cuttingboardS1 == "justbun")
        {
            GetComponent<Transform>().position = new Vector2(-1, -0.8f);
            gameplay.cuttingboardS1 = "fullbun";
        }
        else
                   if (gameplay.cuttingboardS2 == "justbun")
        {
            GetComponent<Transform>().position = new Vector2(1, -0.8f);
            gameplay.cuttingboardS2 = "fullbun";
        }
        else
                   if (gameplay.cuttingboardS3 == "justbun")
        {
            GetComponent<Transform>().position = new Vector2(0, -2f);
            gameplay.cuttingboardS3 = "fullbun";
        }
    }
}
