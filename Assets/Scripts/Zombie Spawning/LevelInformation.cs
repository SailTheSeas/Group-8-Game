using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level Information", menuName = "ScriptableObjects/LevelInformation")]
public class LevelInformation : ScriptableObject
{
    public int numOfWaves;
    public WaveInformation[] waves;

    public int currentWave = 0;
    public Slider WaveSlider;

    public void Reset()
    {
        currentWave = 0;
    }

    public int GetZombieVariations()
    {
        return waves[currentWave].numOfZombieVariations;
    }
    public int[] GetNumZombiesToSpawn()
    {
        return waves[currentWave].numOfTypeOfZombie;
    }

    public ZombieType[] GetZombiesToSpawn()
    {
        return waves[currentWave].zombiesInWave;
    }

    public int GetEnemyCount()
    {
        return waves[currentWave].totalNumOfZombies;
    }

    public bool IsFlagWave()
    {
        return waves[currentWave].isFlag;
    }


    public bool IsFinalWave()
    {
        return currentWave == numOfWaves;
    }

    public void NextWave()
    {
        currentWave++;
        WaveSlider.value = currentWave;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

}
