using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    bool active;
    Keyboard myKeyboard;
    Gamepad myGamePad;
    CharacterController characterController;
    Vector3 movementDirection;
    [SerializeField] private float movSpeed = 1;
    [SerializeField] private float gravity = -9.81f;

    public bool isActive
    {
        get{return active;}
        set{active = value;}
    }
    private void Start()
    {
        Preapare();
    }
    void Update()
    {
        if(active)
        {
            if(myKeyboard!=null)
            {
                CheckInput();
            }
        }
    }
    void Preapare()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR
        myKeyboard = Keyboard.current;
        #endif
        myGamePad = Gamepad.current;
        characterController = GetComponent<CharacterController>();
        active = true;    
    }
    void CheckInput()
    {
      movementDirection = Vector3.zero;
        if (myKeyboard.wKey.isPressed)
        {
            movementDirection += Vector3.forward;
        }
        if (myKeyboard.sKey.isPressed)
        {
            movementDirection += Vector3.back;
        }
        if (myKeyboard.aKey.isPressed)
        {
            movementDirection += Vector3.left;
        }
        if (myKeyboard.dKey.isPressed)
        {
            movementDirection += Vector3.right;
        }
            Gravity();
            movementDirection.Normalize();
            Move(movementDirection);
    }
    void Move(Vector3 direction)
    {
        characterController.Move((direction * 0.1f) * movSpeed);
    }
    void Gravity()
    {
        movementDirection.y = gravity * Time.deltaTime;
    }
}

