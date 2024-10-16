using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInputManager : MonoBehaviour
{
    public static event Action onCharacterPauseOrResume;

    [SerializeField] private float stickDeadZone = 0.2f;
    private int numberPlayer = 0;
    private CharacterMovement characterMovement;
    private InputActionAsset inputAsset;
    private InputActionMap playerMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction handsUpAction;
    private InputAction handsDownAction;
    private InputAction readyAction;
    private InputAction pauseAction;

    private bool isInputEnabled = true;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");
    }
    private void Start()
    {
        characterMovement = this.GetComponent<CharacterMovement>();
        moveAction = playerMap.FindAction(playerMap.actions[0].name);
        jumpAction = playerMap.FindAction(playerMap.actions[1].name);
        handsUpAction = playerMap.FindAction(playerMap.actions[2].name);
        handsDownAction = playerMap.FindAction(playerMap.actions[3].name);
        readyAction = playerMap.FindAction(playerMap.actions[5].name);
        pauseAction = playerMap.FindAction(playerMap.actions[6].name);
    }

    private void Update()
    {   
        if (!isInputEnabled)
        {
            return;
        }
        if(!GameManager.instance.IsGamePaused())
        {
            CharacterTryToMove();
            CharacterTryToJump();
            CharacterTryToMoveHands();
            CharacterTryToGetReady();
        }
        CharacterTryPauseOrResume();
    }
    private void CharacterTryToGetReady()
    {
        bool isTryingToGetReady = readyAction.triggered;
        if (isTryingToGetReady)
        {
            GameManager.instance.SetPlayerReady(numberPlayer);
            Debug.Log("Player " +  numberPlayer + " is ready");
        }
    }
    private void CharacterTryToJump()
    {
        bool isTryingToJump = jumpAction.triggered;
        if (isTryingToJump)
        {
            characterMovement.CharacterTryJump();
        }
    }
    private void CharacterTryToMove()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        if (moveDirection.x > 0 + stickDeadZone)
        {
            characterMovement.CharacterMoveRight();
        }
        else if (moveDirection.x < 0 - stickDeadZone)
        {
            characterMovement.CharacterMoveLeft();
        }
        else
        {
            characterMovement.CharacterMoveIdle();
        }
    }
    private void CharacterTryToMoveHands()
    {
        float handsUp = handsUpAction.ReadValue<float>();
        float handsDown = handsDownAction.ReadValue<float>();
        if (handsUp > 0)
        {
            characterMovement.CharacterMoveHandsUp();
        }
        if (handsDown > 0)
        {
            characterMovement.CharacterMoveHandsDown();
        }
        if (handsUp <= 0 && handsDown <= 0)
        {
            characterMovement.CharacterStopMovingHands();
        }
    }
    private void CharacterTryPauseOrResume()
    {
        bool pauseOrResume = pauseAction.triggered;
        if (pauseOrResume)
        {
            onCharacterPauseOrResume?.Invoke();
        }
    }

    public void DisablePlayerInput()
    {
        isInputEnabled = false;
    }

    public void SetNumberPlayer(int number)
    {
        numberPlayer = number;
    }
}
