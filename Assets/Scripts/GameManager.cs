using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent (typeof(UImanager))]
public class GameManager : MonoBehaviour
{
    public event Action onGameStarted;

    public static bool isGameStarted = false;
    private bool gameIsStarting = false;

    private PlayerManager playerManager;
    private UImanager uiManager;

    private List<bool> playersReady = new List<bool>();

    public static int timerStartTimeSeconds = 3;
    private Timer timer;
    private int timerTicks = 0;
    
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        uiManager = GetComponent<UImanager>();
        timer = GetComponent<Timer>();
    }

    private void FixedUpdate()
    { 
        if(!isGameStarted && playersReady.Count >= 2 && CheckIfAllPlayersAreReady())
        {
            if (!gameIsStarting)
            {
                uiManager.StartCountdownGameStart();
                gameIsStarting = true;
            }
        }
    }

    private void GameStart()
    {
        isGameStarted = true;
        gameIsStarting = false;
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
    private void Timer_onTimerTick()
    {
        timerTicks++;
        if(timerTicks == timerStartTimeSeconds)
        {
            GameStart();
        }
    }
    private void OnEnable()
    {
        playerManager.onPlayerRemoved += OnPlayerRemoved;
        playerManager.onPlayerAdded += OnPlayerAdded;

        timer.onTimerEnd += Timer_onTimerTick;
    }
    private void OnDisable()
    {
        playerManager.onPlayerRemoved -= OnPlayerRemoved;
        playerManager.onPlayerAdded -= OnPlayerAdded;

        timer.onTimerEnd -= Timer_onTimerTick;
    }

    public void SetPlayerReady(int playerNumber)
    {
        playersReady[playerNumber] = true;

        uiManager.UpdateTexts();
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
