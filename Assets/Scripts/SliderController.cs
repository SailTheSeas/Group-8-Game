using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public LevelInformation levelInfo;
    public Slider waveSlider;

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
            waveSlider.value = levelInfo.GetCurrentWave();
        }
    }
}
