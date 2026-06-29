using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        uiSlider.onValueChanged.AddListener(SetUIVolume);
    }

    private void SetMasterVolume(float value)
    {
        SoundManager.SetSoundVolume("MasterVolume", value);
    }

    private void SetMusicVolume(float value)
    {
        SoundManager.SetSoundVolume("MusicVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        SoundManager.SetSoundVolume("SFXVolume", value);
    }
    private void SetUIVolume(float value)
    {
        SoundManager.SetSoundVolume("UIVolume", value);
    }
}