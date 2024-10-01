using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private float stickDeadZone = 0.2f;

    private CharacterMovement characterMovement;
    private InputActionAsset inputAsset;
    private InputActionMap playerMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction handsUpAction;
    private InputAction handsDownAction;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");
    }

    void Start()
    {
        characterMovement = character.GetComponent<CharacterMovement>();
        moveAction = playerMap.FindAction(playerMap.actions[0].name);
        jumpAction = playerMap.FindAction(playerMap.actions[1].name);
        handsUpAction = playerMap.FindAction(playerMap.actions[2].name);
        handsDownAction = playerMap.FindAction(playerMap.actions[3].name);
    }

    void Update()
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

        bool isTryingToJump = jumpAction.triggered;
        if(isTryingToJump)
        {
            characterMovement.CharacterTryJump();
        }

        float handsUp = handsUpAction.ReadValue<float>();
        if (handsUp > 0)
        {
            characterMovement.CharacterMoveHandsUp();
        }
        float handsDown = handsDownAction.ReadValue<float>();
        if (handsDown > 0)
        {
            characterMovement.CharacterMoveHandsDown();
        }
    }
}
