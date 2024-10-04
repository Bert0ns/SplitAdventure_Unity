using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class GameManager : MonoBehaviour
{
    public event Action onGameStarted;

    public static bool isGameStarted = false;
    private PlayerManager playerManager;
    private List<bool> playersReady = new List<bool>();
    
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        playerManager.onPlayerRemoved += OnPlayerRemoved;
        playerManager.onPlayerAdded += OnPlayerAdded;
    }
    private void OnDisable()
    {
        playerManager.onPlayerRemoved -= OnPlayerRemoved;
        playerManager.onPlayerAdded -= OnPlayerAdded;
    }

    void FixedUpdate()
    {
        if(!isGameStarted && playersReady.Count >= 2 && CheckIfAllPlayersAreReady())
        {
            GameStart();
        }
    }

    private void GameStart()
    {
        isGameStarted = true;
        Debug.Log("Game Started");
        onGameStarted?.Invoke();
    }
    private bool CheckIfAllPlayersAreReady()
    {
        foreach (var item in playersReady)
        {
            if (item != true)
            {
                return false;
            }
        }
        return true;
    }

    private void OnPlayerAdded()
    {
        playersReady.Add(false);
    }
    private void OnPlayerRemoved(int playerNumber)
    {
        playersReady.RemoveAt(playerNumber);
    }

    public void SetPlayerReady(int playerNumber)
    {
        playersReady[playerNumber] = true;
    }

    public int GetNumberPlayersReady()
    {
        int count = 0;
        foreach (var item in playersReady)
        {
            if (item)
                count++;
        }
        return count;
    }
    public int GetNumberOfPlayers()
    {
        return playersReady.Count;
    }
}
