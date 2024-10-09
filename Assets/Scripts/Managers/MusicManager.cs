using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume = 0.2f;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private float menuMusicPitch = 1.2f;

    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private float battleMusicPitch = 0.9f;

    private float previousClipTime = 0f;
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
        PlayMusic(battleMusic, battleMusicPitch, volume);
    }

    private void OnGamePaused()
    {
        PlayMusic(menuMusic, menuMusicPitch, volume);
    }

    private void OnGameEnded()
    {
        PlayMusic(menuMusic, menuMusicPitch, volume);
    }

    private void OnGameStarted()
    {
        PlayMusic(battleMusic, battleMusicPitch, volume);
    }

    private void PlayMusic(AudioClip music, float pitch, float volume)
    {
        audioSource.Pause();
        float clipTime = audioSource.time;
        audioSource.clip = music;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.time = previousClipTime;
        audioSource.Play();
        previousClipTime = clipTime;
    }
}
