using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Text soundValueText;

    private void OnEnable()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundValue", 100.0f);
    }

    public void OnSoundValueChanged()
    {
        soundValueText.text = (int) soundSlider.value + "%";
        PlayerPrefs.SetFloat("SoundValue", soundSlider.value);
        AudioListener.volume = soundSlider.value / 100f;
    }
}
