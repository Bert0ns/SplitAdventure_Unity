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
    public event Action onGamePaused;
    public event Action onGameResumed;

    public static GameManager instance;

    public static bool isGameStarted = false;
    private bool gameIsStarting = false;
    private bool isGamePaused = false;

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
    private void OnCharacterPause()
    {
        if(!isGameStarted)
        {
            return;
        }

        if(isGamePaused)
        {
            GameResume();
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 0;
            onGamePaused?.Invoke();
        }
    }
    private void OnEnable()
    {
        PlayerManager.instance.onPlayerRemoved += OnPlayerRemoved;
        PlayerManager.instance.onPlayerAdded += OnPlayerAdded;

        timer.onTimerEnd += Timer_onTimerTick;

        CharacterInputManager.onCharacterPauseOrResume += OnCharacterPause;
    }

    private void OnDisable()
    {
        PlayerManager.instance.onPlayerRemoved -= OnPlayerRemoved;
        PlayerManager.instance.onPlayerAdded -= OnPlayerAdded;

        timer.onTimerEnd -= Timer_onTimerTick;

        CharacterInputManager.onCharacterPauseOrResume -= OnCharacterPause;
    }
    private void ResetState()
    {
        isGameStarted = false;
        Time.timeScale = 1f;
    }

    public void SetPlayerReady(int playerNumber)
    {
        if(UImanager.instance.IsOptionsMenuOpen())
        {
            return;
        }

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

    public void GameResume()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        onGameResumed?.Invoke();
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    
}
