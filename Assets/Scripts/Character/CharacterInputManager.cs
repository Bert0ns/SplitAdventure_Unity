using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    public event Action OnMoveAction;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();
        float isTryingToJump = jumpAction.action.ReadValue<float>();
    }
}
