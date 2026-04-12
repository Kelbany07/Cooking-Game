using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bincontrol : MonoBehaviour {
    public Transform bottombunObj;
    public Transform topbunObj;
    public Transform burgerObj;
    public Transform backrollObj;
    public Transform frontrollObj;
    public Transform hotdogObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (gameObject.name == "bun bin")
        {
            if (gameplay.cuttingboardS1 == "empty")
            {
                Instantiate(bottombunObj, new Vector2(-1, -0.8f), bottombunObj.rotation);
                Instantiate(topbunObj, new Vector2(-1, -0.6f), topbunObj.rotation);
                gameplay.cuttingboardS1 = "full";
            }
            else
            if (gameplay.cuttingboardS2 == "empty")
            {
                Instantiate(bottombunObj, new Vector2(1, -0.8f), bottombunObj.rotation);
                Instantiate(topbunObj, new Vector2(1, -0.6f), topbunObj.rotation);
                gameplay.cuttingboardS2 = "full";
            }
            else
            if (gameplay.cuttingboardS3 == "empty")
            {
                Instantiate(bottombunObj, new Vector2(0, -2f), bottombunObj.rotation);
                Instantiate(topbunObj, new Vector2(0, -1.80f), topbunObj.rotation);
                gameplay.cuttingboardS3 = "full";
            }
        }
        if (gameObject.name == "hamburgers")
        {
            if (gameplay.grillS1 == "empty")
            {
                Instantiate(burgerObj, new Vector2(5, -1.03f), burgerObj.rotation);
                gameplay.grillS1 = "full";
            }
            else
                if (gameplay.grillS2 == "empty")
            {
                Instantiate(burgerObj, new Vector2(7, -1.03f), burgerObj.rotation);
                gameplay.grillS2 = "full";
            }
            else
                if (gameplay.grillS3 == "empty")
            {
                Instantiate(burgerObj, new Vector2(7, -2.24f), burgerObj.rotation);
                gameplay.grillS3 = "full";
            }
            {
                
            }
        }
    }
}
