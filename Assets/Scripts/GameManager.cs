using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent (typeof(UImanager))]
public class GameManager : MonoBehaviour
{
    public event Action onGameStarted;
    public event Action onGameEnded;

    public static GameManager instance;

    public static bool isGameStarted = false;
    private bool gameIsStarting = false;

    private List<bool> playersReady = new List<bool>();
    private List<bool> playersAlive = new List<bool>();

    public static int timerStartTimeSeconds = 3;
    private Timer timer;
    private int timerTicks = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        timer = GetComponent<Timer>();
    }

    private void FixedUpdate()
    { 
        if(!isGameStarted && playersReady.Count >= 2 && CheckIfAllPlayersAreReady())
        {
            if (!gameIsStarting)
            {
                UImanager.instance.StartCountdownGameStart();

                playersAlive.Clear();
                playersReady.ForEach(p => playersAlive.Add(true));
                
                gameIsStarting = true;
                Debug.Log("Game is starting...");
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
    private void GameEnd()
    {
        Debug.Log("Game End");
        onGameEnded?.Invoke();
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
    private bool CheckIfOnlyOnePlayerIsALive()
    {
        int numPlayersAlive = 0;
        foreach (var item in playersAlive)
        {
            if (item == true)
            {
                numPlayersAlive++;
            }
            if (numPlayersAlive >= 2)
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
        PlayerManager.instance.onPlayerRemoved += OnPlayerRemoved;
        PlayerManager.instance.onPlayerAdded += OnPlayerAdded;

        timer.onTimerEnd += Timer_onTimerTick;
    }
    private void OnDisable()
    {
        PlayerManager.instance.onPlayerRemoved -= OnPlayerRemoved;
        PlayerManager.instance.onPlayerAdded -= OnPlayerAdded;

        timer.onTimerEnd -= Timer_onTimerTick;
    }
    private void ResetState()
    {
        isGameStarted = false;
        //playersAlive.Clear();
        //playersReady.Clear();
    }

    public void SetPlayerReady(int playerNumber)
    {
        playersReady[playerNumber] = true;

        UImanager.instance.UpdateTexts();
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

    public void PlayerNDied(int index_n)
    {
        playersAlive[index_n] = false;

        if(CheckIfOnlyOnePlayerIsALive())
        {
            GameEnd();
        }
    } 

    public void QuitApplication()
    {
        Debug.Log("Quit Application");
        Application.Quit(); 
    }

    public void RestartGame()
    {
        ResetState();

        SceneManager.LoadScene(0);
    }

}
