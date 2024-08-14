using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPickup : MonoBehaviour
{
    private SunController sunController;
    // Start is called before the first frame update
    void Start()
    {
        sunController = GameObject.Find("Canvas").GetComponent<SunController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sunController.PickupSun();
            //Destroy(gameObject);
        }
    }
}
