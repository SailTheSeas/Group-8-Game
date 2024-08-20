using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunController : MonoBehaviour
{
    public TextMeshProUGUI SunText;
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
        SunText.text = $"{sunAmount}";
    }

    public void SpendSun(int SunCost)
    {
        if (sunAmount >= SunCost)
        {
            sunAmount -= SunCost;
            SunText.text = $"{sunAmount}";
        }
    }

    public int GetSun()
    {
        return sunAmount;
    }
}
