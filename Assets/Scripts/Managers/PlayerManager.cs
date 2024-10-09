using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public event Action onPlayerAdded;
    public event Action<int> onPlayerRemoved;

    public static PlayerManager instance;

    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<Color> playerColors;

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private PlayerInputManager playerInputManager;

    [SerializeField] private List<Sprite> hatSprites;

    [SerializeField] private AudioClip playerJoinedAudioClip;
    [SerializeField] private AudioClip playerLeftAudioClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeft; 
    }
    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= OnPlayerJoined;
        playerInputManager.onPlayerLeft -= OnPlayerLeft;
    }
    private void OnPlayerJoined(PlayerInput player)
    {
        playerInputs.Add(player);
        player.tag = "Player" + (playerInputs.Count - 1);

        CharacterInputManager chInMan = player.GetComponent<CharacterInputManager>();

        onPlayerAdded?.Invoke();
        
        chInMan.SetNumberPlayer(playerInputs.Count - 1);

        AssignSpawnPoint(player.transform, startingPoints[(playerInputs.Count - 1) % startingPoints.Count]);
        
        var spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();
        AssignColor(spriteRenderers, new List<int> { 2, 3, 4, 5 }, playerColors.ElementAt((playerInputs.Count - 1) % playerColors.Count));
        AssignHat(spriteRenderers, 1, hatSprites, (playerInputs.Count - 1) % hatSprites.Count);

        player.gameObject.GetComponent<CharacterLife>().onPlayerDeath += OnPlayerDeath;

        SoundFXManager.Instance.PlaySoundFXClip(playerJoinedAudioClip, player.transform, 1f);
    }
    private void AssignSpawnPoint(Transform player, Transform spawnPoint)
    {
        player.position = spawnPoint.position;
    }
    private void AssignColor(SpriteRenderer[] spriteRenderers, List<int> renderersIndex, Color color)
    {
        foreach (int rendererIndex in renderersIndex)
        {
            spriteRenderers[rendererIndex].color = color;
        }
    }
    private void AssignHat(SpriteRenderer[] spriteRenderers, int rendererIndex, List<Sprite> hatSprites, int hatIndex)
    {
        spriteRenderers[rendererIndex].sprite = hatSprites[hatIndex];
    }
    private void OnPlayerLeft(PlayerInput player)
    {
        int index = playerInputs.IndexOf(player);
        playerInputs.RemoveAt(index);
        onPlayerRemoved?.Invoke(index);
        player.gameObject.GetComponent<CharacterLife>().onPlayerDeath -= OnPlayerDeath;

        SoundFXManager.Instance.PlaySoundFXClip(playerJoinedAudioClip, player.transform, 1f);
    }
    private void OnPlayerDeath(PlayerInput obj)
    {
        GameManager.instance.PlayerNDied(playerInputs.IndexOf(obj));
    }
}
