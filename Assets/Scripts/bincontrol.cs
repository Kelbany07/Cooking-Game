using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bincontrol : MonoBehaviour
{
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

    // helper to set occupiedSlot on whatever script exists on the spawned transform
    void SetSlotOnInstance(Transform t, int slot)
    {
        if (t == null) return;
        var ms = t.GetComponent<movesandwich>();
        if (ms != null) ms.occupiedSlot = slot;
        var mt = t.GetComponent<movetoppings>();
        if (mt != null) mt.occupiedSlot = slot;
        var cf = t.GetComponent<cookfood>();
        if (cf != null) cf.occupiedSlot = slot;
    }

    void OnMouseDown()
    {
        if (gameObject.name == "bun bin")
        {
            if (gameplay.cuttingboardS1 == "empty")
            {
                var b = Instantiate(bottombunObj, new Vector2(-1, -0.8f), bottombunObj.rotation);
                var t = Instantiate(topbunObj, new Vector2(-1, -0.6f), topbunObj.rotation);
                SetSlotOnInstance(b, 1);
                SetSlotOnInstance(t, 1);
                gameplay.cuttingboardS1 = "justbun";
                gameplay.SelectedSlot = 1;
            }
            else if (gameplay.cuttingboardS2 == "empty")
            {
                var b = Instantiate(bottombunObj, new Vector2(1, -0.8f), bottombunObj.rotation);
                var t = Instantiate(topbunObj, new Vector2(1, -0.6f), topbunObj.rotation);
                SetSlotOnInstance(b, 2);
                SetSlotOnInstance(t, 2);
                gameplay.cuttingboardS2 = "justbun";
                gameplay.SelectedSlot = 2;
            }
            else if (gameplay.cuttingboardS3 == "empty")
            {
                var b = Instantiate(bottombunObj, new Vector2(0, -2f), bottombunObj.rotation);
                var t = Instantiate(topbunObj, new Vector2(0, -1.80f), topbunObj.rotation);
                SetSlotOnInstance(b, 3);
                SetSlotOnInstance(t, 3);
                gameplay.cuttingboardS3 = "justbun";
                gameplay.SelectedSlot = 3;
            }
        }

        if (gameObject.name == "hamburgers")
        {
            if (gameplay.grillS1 == "empty")
            {
                var p = Instantiate(burgerObj, new Vector2(5, -1.03f), burgerObj.rotation);
                SetSlotOnInstance(p, 1);
                gameplay.grillS1 = "full";
            }
            else if (gameplay.grillS2 == "empty")
            {
                var p = Instantiate(burgerObj, new Vector2(7, -1.03f), burgerObj.rotation);
                SetSlotOnInstance(p, 2);
                gameplay.grillS2 = "full";
            }
            else if (gameplay.grillS3 == "empty")
            {
                var p = Instantiate(burgerObj, new Vector2(7, -2.24f), burgerObj.rotation);
                SetSlotOnInstance(p, 3);
                gameplay.grillS3 = "full";
            }
        }

        if (gameObject.name == "hotdog bin")
        {
            if (gameplay.grillS1 == "empty")
            {
                var p = Instantiate(hotdogObj, new Vector2(5, -1.03f), hotdogObj.rotation);
                SetSlotOnInstance(p, 1);
                gameplay.grillS1 = "full";
            }
            else if (gameplay.grillS2 == "empty")
            {
                var p = Instantiate(hotdogObj, new Vector2(7, -1.03f), hotdogObj.rotation);
                SetSlotOnInstance(p, 2);
                gameplay.grillS2 = "full";
            }
            else if (gameplay.grillS3 == "empty")
            {
                var p = Instantiate(hotdogObj, new Vector2(7, -2.24f), hotdogObj.rotation);
                SetSlotOnInstance(p, 3);
                gameplay.grillS3 = "full";
            }
        }

        if (gameObject.name == "trash bin")
        {
            gameplay.deleteFood = "y";
        }
    }
}
