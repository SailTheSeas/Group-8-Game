using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] zombiePrefabs;
    [SerializeField] private LevelInformation[] levels;
    [SerializeField] private float startDelay, waveDelay, levelDelay;
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private int numOfLevels;
    [SerializeField] private GameObject flagAnnouncment;
    [SerializeField] private UIController slider;
    private int zombieCount, levelCount, difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 1;
        zombieCount = 0;
        levelCount = 0;
        levels[levelCount].Reset();
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWave()
    {
        zombieCount += levels[levelCount].GetEnemyCount();
        ZombieType[] zombiesToSpawn = levels[levelCount].GetZombiesToSpawn();
        int[] numZombiesToSpawn = levels[levelCount].GetNumZombiesToSpawn();
        int zombieVariations = levels[levelCount].GetZombieVariations();
        for (int k = 0; k < difficulty; k++)
        {
            for (int i = 0; i < zombieVariations; i++)
            {
                for (int j = 0; j < numZombiesToSpawn[i]; j++)
                {
                    StartCoroutine(SpawnDelay(GetZombieToSpawn(zombiesToSpawn[i])));
                    /*int row = Random.Range(0, 4);
                    GameObject zombie = Instantiate(GetZombieToSpawn(zombiesToSpawn[i]), spawnPositions[row], Quaternion.identity);
                    zombie.GetComponent<ZombieBehaviours>().SetRow(row);
                    zombie.GetComponent<ZombieBehaviours>().SetZombieSpawner(this.GetComponent<ZombieSpawner>());*/

                }

            }
        }
        if (levels[levelCount].IsFlagWave(0) == false)
            if (levels[levelCount].IsFlagWave(1) == false)
            StartCoroutine(WaveDelayFirst());
    }

    private void SpawnZombie(GameObject zombieToSpawn)
    {
        int row = Random.Range(0, 4);
        float offset = Random.Range(0, 4);
        Vector3 offsetV = new Vector3(offset, 0, 0);
        GameObject zombie = Instantiate(zombieToSpawn, spawnPositions[row] + offsetV, Quaternion.identity);
        zombie.GetComponent<ZombieBehaviours>().SetRow(row);
        zombie.GetComponent<ZombieBehaviours>().SetZombieSpawner(this.GetComponent<ZombieSpawner>());
    }

    public void KillZombie()
    {
        zombieCount--;
        if (zombieCount <= 0)
        {
            StopCoroutine(WaveDelayFirst());
            if (levels[levelCount].IsFinalWave())
            {
                levelCount++;
                if (levelCount == numOfLevels)
                {
                    levelCount = 0;
                    difficulty++;
                }
                levels[levelCount].Reset();
                StartCoroutine(LevelDelay());
            } else
            {
                levels[levelCount].NextWave();
                slider.UpdateLevelData(levels[levelCount]);
                if (levels[levelCount].IsFlagWave(0) == true)
                {
                    flagAnnouncment.SetActive(true);
                    StartCoroutine(FlagDelay());
                }
                StartCoroutine(WaveDelay());
            }
        }
    }

    IEnumerator FlagDelay()
    {
        yield return new WaitForSeconds(2f);
        flagAnnouncment.SetActive(false);
    }

    IEnumerator SpawnDelay(GameObject zombieToSpawn)
    {
        yield return new WaitForSeconds(0f);
        SpawnZombie(zombieToSpawn);
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        SpawnWave();
    }

    IEnumerator WaveDelayFirst()
    {
        yield return new WaitForSeconds(waveDelay*10);
        SpawnWave();
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        SpawnWave();
    }

    IEnumerator LevelDelay()
    {
        yield return new WaitForSeconds(levelDelay);
        SpawnWave();
    }

    

    private GameObject GetZombieToSpawn(ZombieType zombieType)
    {
        int zombiePrefabIndex;

        switch (zombieType)
        {
            case ZombieType.Basic:
                zombiePrefabIndex = 0;
                break;
            case ZombieType.Cone:
                zombiePrefabIndex = 1;
                break;
            case ZombieType.Bucket:
                zombiePrefabIndex = 2;
                break;
            case ZombieType.Door:
                zombiePrefabIndex = 3;
                break;
            case ZombieType.Pole:
                zombiePrefabIndex = 4;
                break;
            case ZombieType.Rugby:
                zombiePrefabIndex = 5;
                break;
            default:
                zombiePrefabIndex = 0;
                break;
        }

        return zombiePrefabs[zombiePrefabIndex];
    }
}
