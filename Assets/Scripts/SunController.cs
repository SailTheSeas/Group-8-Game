using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    
    public int sunAmount;
    // Start is called before the first frame update
    void Start()
    {
        sunAmount = 50;
    }

    // Update is called once per frame
    

    public void PickupSun()
    {
        sunAmount += 25;
    }

    public void SpendSun(int SunCost)
    {
        if (sunAmount >= SunCost)
        {
            sunAmount -= SunCost;
        }
    }

    public int GetSun()
    {
        return sunAmount;
    }
}
