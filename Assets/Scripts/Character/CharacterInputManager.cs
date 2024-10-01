using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference handsUpAction;
    [SerializeField] private InputActionReference handsDownAction;
    [SerializeField] private GameObject character;
    [SerializeField] private float stickDeadZone = 0.2f;

    private CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = character.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();
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

        bool isTryingToJump = jumpAction.action.triggered;
        if(isTryingToJump)
        {
            characterMovement.CharacterTryJump();
        }

        float handsUp = handsUpAction.action.ReadValue<float>();
        if (handsUp > 0)
        {
            characterMovement.CharacterMoveHandsUp();
        }
        float handsDown = handsDownAction.action.ReadValue<float>();
        if (handsDown > 0)
        {
            characterMovement.CharacterMoveHandsDown();
        }
    }
}
