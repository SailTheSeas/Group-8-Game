using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIController : MonoBehaviour
{
    public LevelInformation levelInfo;
    public Slider waveSlider;
    public float SliderValue;
    //public GameObject WaveText;
    public bool CreatedText = false;
    public GameObject TextPrefab;

    void Start()
    {
        if (waveSlider == null)
        {
            waveSlider = GameObject.Find("WaveSlider").GetComponent<Slider>();
        }
    }

    void Update()
    {
        if (waveSlider != null && levelInfo != null)
        {
            waveSlider.maxValue = levelInfo.GetWaveCount();
            waveSlider.value = levelInfo.GetCurrentWave();
        }

        /*if (levelInfo.FlagLevel && !CreatedText)
        {
            //Debug.Log("Flag Level");
            SliderValue = SliderValue + Time.deltaTime * 0.65f;
            waveSlider.value = SliderValue;
            GameObject Canvas = GameObject.Find("Canvas");
            TextPrefab = Instantiate(WaveText, Canvas.transform.position, Quaternion.identity, Canvas.transform);
            CreatedText = true;
            StartCoroutine(DestroyText(TextPrefab));
        }*/
    }

    public void UpdateLevelData(LevelInformation newLevelInfo)
    {
        levelInfo = newLevelInfo;
    }

    IEnumerator DestroyText(GameObject TextPrefab)
    {
        yield return new WaitForSeconds(2f);
        Destroy(TextPrefab);
    }
}
