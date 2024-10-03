using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class GameManager : MonoBehaviour
{
    public static bool isGameStarted = false;
    private PlayerManager playerManager;
    private List<bool> playersReady = new List<bool>();
    
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
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

    public void PlayerAdded()
    {
        playersReady.Add(false);
    }
    public void PlayerRemoved(int playerNumber)
    {
        playersReady.RemoveAt(playerNumber);
    }

    public void SetPlayerReady(int playerNumber)
    {
        playersReady[playerNumber] = true;
    }
}
