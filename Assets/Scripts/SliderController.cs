using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public LevelInformation levelInfo;
    public Slider waveSlider;
    public float SliderValue;

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
            //waveSlider.value = levelInfo.GetCurrentWave();
        }

        if (levelInfo.FlagLevel)
        {
            Debug.Log("Flag Level");
            SliderValue = SliderValue + Time.deltaTime * 0.65f;
            waveSlider.value = SliderValue;
        }
    }
}
