using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght);
    }
    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips == null || audioClips.Length <= 0)
        {
            return;
        }
        int rand = Random.Range(0, audioClips.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClips[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght);
    }
}
