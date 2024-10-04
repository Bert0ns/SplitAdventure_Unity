using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class UImanager : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI numberOfPlayersText;
    [SerializeField] private TextMeshProUGUI numberOfPlayersReadyText;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        uiPanel.SetActive(true);
        numberOfPlayersText.text = "0";
        numberOfPlayersReadyText.text = "0/0";
    }
    private void FixedUpdate()
    {
        int numberPlayers = gameManager.GetNumberOfPlayers();
        int numberPlayersReady = gameManager.GetNumberPlayersReady();

        numberOfPlayersText.text = numberPlayers.ToString();
        numberOfPlayersReadyText.text = numberPlayersReady + "/" + numberPlayers; 
    }

    private void OnEnable()
    {
        gameManager.onGameStarted += OnGameStarted;
    }
    private void OnDisable()
    {
        gameManager.onGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        uiPanel.SetActive(false);
    }
}
