using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Information", menuName = "ScriptableObjects/PlantStats")]
public class PlantStats : ScriptableObject
{
    public float[] rechargeTime;
    public int[] plantCost;
}
