using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSliderInitialStateUpdater : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundFXSlider;

    private void Start()
    {
        if (masterSlider != null)
            masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        if (soundFXSlider != null)
            soundFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume", 1f);
    }
}
