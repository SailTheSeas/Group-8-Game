using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Information", menuName = "ScriptableObjects/WaveInformation")]
public class WaveInformation : ScriptableObject
{
    
    public int totalNumOfZombies;
    public int numOfZombieVariations;
    public int[] numOfTypeOfZombie;
    public ZombieType[] zombiesInWave;
    public bool isFlag;
   
}
