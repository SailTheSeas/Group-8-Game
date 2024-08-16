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
    

    public void PickupSun()
    {
        SunAmount += 25;
    }

    public void SpendSun(int SunCost)
    {
        if (SunAmount >= SunCost)
        {
            SunAmount -= SunCost;
        }
    }
}
