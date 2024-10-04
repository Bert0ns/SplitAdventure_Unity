using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent (typeof(Timer))]
public class UImanager : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI numberOfPlayersText;
    [SerializeField] private TextMeshProUGUI numberOfPlayersReadyText;
    [SerializeField] private TextMeshProUGUI countdownText;

    private GameManager gameManager;
    private PlayerManager playerManager;
    private Timer timer;
    private int timerTicks = 0;
   
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        playerManager = GetComponent<PlayerManager>();
        timer = GetComponent<Timer>();
    }
    private void Start()
    {
        uiPanel.SetActive(true);
        countdownText.gameObject.SetActive(false);
        numberOfPlayersText.text = "0";
        numberOfPlayersReadyText.text = "0/0";
    }

    private void OnEnable()
    {
        gameManager.onGameStarted += OnGameStarted;
        playerManager.onPlayerAdded += OnPlayerAdded;
        playerManager.onPlayerRemoved += OnPlayerRemoved;
    }
    private void OnDisable()
    {
        gameManager.onGameStarted -= OnGameStarted;
        playerManager.onPlayerAdded -= OnPlayerAdded;
        playerManager.onPlayerRemoved -= OnPlayerRemoved;
    }
    private void OnPlayerRemoved(int obj)
    {
        UpdateTexts();
    }
    private void OnPlayerAdded()
    {
        UpdateTexts();
    }
    private void OnGameStarted()
    {
        uiPanel.SetActive(false);
    }

    public void UpdateTexts()
    {
        int numberPlayers = gameManager.GetNumberOfPlayers();
        int numberPlayersReady = gameManager.GetNumberPlayersReady();

        numberOfPlayersText.text = numberPlayers.ToString();
        numberOfPlayersReadyText.text = numberPlayersReady + "/" + numberPlayers;
    }

    public void StartCountdownGameStart()
    {
        timer.SetDuration(1f);
        timer.onTimerEnd += Timer_onTimerEnd;
        timerTicks = GameManager.timerStartTimeSeconds;
        countdownText.text = timerTicks.ToString();
        countdownText.gameObject.SetActive(true);

        UIAnimationManager.instance.PlayStartAnimation();

        timer.StartTimer();
    }

    private void Timer_onTimerEnd()
    {
        timerTicks -= 1;
        countdownText.text = timerTicks.ToString();
        if(timerTicks > 0)
        {
            timer.StartTimer();
        }
    }
}
