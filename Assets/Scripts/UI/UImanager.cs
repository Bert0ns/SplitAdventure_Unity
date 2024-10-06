using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GameManager))]
[RequireComponent (typeof(Timer))]
public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    [SerializeField] private GameObject panelWaitingForPlayers;
    [SerializeField] private TextMeshProUGUI numberOfPlayersText;
    [SerializeField] private TextMeshProUGUI numberOfPlayersReadyText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private GameObject panelGameEnded;
    [SerializeField] private GameObject gameEndFirstButton;

    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelPauseFirstButton;

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
    private void Start()
    {
        panelWaitingForPlayers.SetActive(true);
        countdownText.gameObject.SetActive(false);
        numberOfPlayersText.text = "0";
        numberOfPlayersReadyText.text = "0/0";

        panelGameEnded.SetActive(false);
        panelPause.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.instance.onGameStarted += OnGameStarted;
        GameManager.instance.onGameEnded += OnGameEnded;

        GameManager.instance.onGamePaused += OnGamePaused;
        GameManager.instance.onGameResumed += OnGameResumed;

        PlayerManager.instance.onPlayerAdded += OnPlayerAdded;
        PlayerManager.instance.onPlayerRemoved += OnPlayerRemoved;
    }
    private void OnDisable()
    {
        GameManager.instance.onGameStarted -= OnGameStarted;
        GameManager.instance.onGameEnded -= OnGameEnded;

        GameManager.instance.onGamePaused -= OnGamePaused;
        GameManager.instance.onGameResumed -= OnGameResumed;

        PlayerManager.instance.onPlayerAdded -= OnPlayerAdded;
        PlayerManager.instance.onPlayerRemoved -= OnPlayerRemoved;
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
        panelWaitingForPlayers.SetActive(false);
    }
    private void OnGameEnded()
    {
        panelGameEnded.SetActive(true);
        SelectFirstUIItem(gameEndFirstButton);
    }
    private void OnGameResumed()
    {
        panelPause.SetActive(false);
    }
    private void OnGamePaused()
    {
        panelPause.SetActive(true);
        SelectFirstUIItem(panelPauseFirstButton);
    }
    private void SelectFirstUIItem(GameObject item)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(item);
    }
    private void Timer_onTimerEnd()
    {
        timerTicks -= 1;
        countdownText.text = timerTicks.ToString();
        if (timerTicks > 0)
        {
            timer.StartTimer();
        }
    }

    public void UpdateTexts()
    {
        int numberPlayers = GameManager.instance.GetNumberOfPlayers();
        int numberPlayersReady = GameManager.instance.GetNumberPlayersReady();

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
}
