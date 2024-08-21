using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level Information", menuName = "ScriptableObjects/LevelInformation")]
public class LevelInformation : ScriptableObject
{
    public int numOfWaves;
    public WaveInformation[] waves;
    public Slider WaveSlider;
    public int currentWave = 0;
    public bool FlagLevel = false;
    

    void Start()
    {
        //WaveSlider = GameObject.Find("WaveSlider").GetComponent<Slider>();
        WaveSlider = FindObjectOfType<Slider>();
    }
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
        FlagLevel = true;
        return waves[currentWave].isFlag;
    }


    public bool IsFinalWave()
    {
        return currentWave == numOfWaves;
    }

    public void NextWave()
    {
        currentWave++;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    void Update()
    {
        WaveSlider.value = currentWave;
    }

}
