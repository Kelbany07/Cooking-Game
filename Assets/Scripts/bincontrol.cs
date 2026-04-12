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
    private void OnMouseDown()
    {
        if ((gameObject.name == "bun bin"))
        {
         Instantiate(bottombunObj, new Vector2(0, -0.8f), bottombunObj.rotation);
            Instantiate(topbunObj, new Vector2(0, -0.6f), topbunObj.rotation);
        }
    }
}
