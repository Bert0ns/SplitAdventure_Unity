using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip battleMusic;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        Invoke(nameof(SubscribeToEvents), 0.2f);
    }
    private void SubscribeToEvents()
    {
        GameManager.instance.onGameStarted += OnGameStarted;
        GameManager.instance.onGameEnded += OnGameEnded;
        GameManager.instance.onGamePaused += OnGamePaused;
        GameManager.instance.onGameResumed += OnGameResumed;
    }
    private void OnDisable()
    {
        GameManager.instance.onGameStarted -= OnGameStarted;
        GameManager.instance.onGameEnded -= OnGameEnded;
        GameManager.instance.onGamePaused -= OnGamePaused;
        GameManager.instance.onGameResumed -= OnGameResumed;
    }

    private void OnGameResumed()
    {
        PlayBattleMusic();
    }

    private void OnGamePaused()
    {
        PlayMenuMusic();
    }

    private void OnGameEnded()
    {
        PlayMenuMusic();
    }

    private void OnGameStarted()
    {
        PlayBattleMusic();
    }

    private void PlayMenuMusic()
    {
        audioSource.Stop();
        audioSource.clip = menuMusic;
        audioSource.pitch = 1.2f;
        audioSource.Play();
    }
    private void PlayBattleMusic()
    {
        audioSource.Stop();
        audioSource.clip = battleMusic;
        audioSource.pitch = 0.9f;
        audioSource.Play();
    }
}
