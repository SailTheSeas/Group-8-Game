using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public int SunAmount;
    // Start is called before the first frame update
    void Start()
    {
        SunAmount = 50;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Plant Spawned");
            SpendSun(40);
        }*/
    }

    public void PickupSun()
    {
        SunAmount = SunAmount + 25;
    }

    public void SpendSun(int SunCost)
    {
        if (SunAmount > SunCost)
        {
            SunAmount = SunAmount - SunCost;
        }
    }
}
