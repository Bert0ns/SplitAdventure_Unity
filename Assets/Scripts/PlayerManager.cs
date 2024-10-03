using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<Color> playerColors;

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private PlayerInputManager playerInputManager;
    private void Awake()
    {
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
        GameManager gm = this.GetComponent<GameManager>();
        gm.PlayerAdded();
        chInMan.SetGameManager(gm);
        chInMan.SetNumberPlayer(playerInputs.Count - 1);

        player.transform.position = startingPoints[(playerInputs.Count - 1) % startingPoints.Count].position;

        var spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();
        spriteRenderers[1].color = playerColors.ElementAt(playerInputs.Count - 1);
        spriteRenderers[2].color = playerColors.ElementAt(playerInputs.Count - 1);
        spriteRenderers[3].color = playerColors.ElementAt(playerInputs.Count - 1);
        spriteRenderers[4].color = playerColors.ElementAt(playerInputs.Count - 1);
    }
    private void OnPlayerLeft(PlayerInput player)
    {
        GameManager gm = this.GetComponent<GameManager>();
        int index = playerInputs.IndexOf(player);
        playerInputs.RemoveAt(index);
        gm.PlayerRemoved(index);
    }

    public int GetNumberOfPlayers()
    {
        return playerInputs.Count;
    }
}
