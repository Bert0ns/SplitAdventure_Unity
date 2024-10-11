using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private List<string> soundGroups = new List<string> { "masterVolume", "musicVolume", "soundFXVolume" };

    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(soundGroups[0], 1f));
        SetMusicVolume(PlayerPrefs.GetFloat (soundGroups[1], 1f));
        SetSoundFXVolume(PlayerPrefs.GetFloat(soundGroups[2], 1f));
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat(soundGroups[0], Mathf.Log10(level) * 20f);
        SavePlayerPrefs(soundGroups[0], level);
    }
    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat(soundGroups[2], Mathf.Log10(level) * 20f);
        SavePlayerPrefs(soundGroups[2], level);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat(soundGroups[1], Mathf.Log10(level) * 20f);
        SavePlayerPrefs(soundGroups[1], level);
    }

    private void SavePlayerPrefs(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }
}
