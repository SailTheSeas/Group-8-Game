using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject SunShine;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSkySun());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnSkySun()
    {
        yield return new WaitForSeconds(10f);
        Vector2 SpawnPosition = new Vector2(Random.Range(-5f, 7f), 6f);
        Instantiate(SunShine, SpawnPosition, Quaternion.identity);
        Debug.Log("Drop Sunshine");
        StartCoroutine(SpawnSkySun());
    }    
}
