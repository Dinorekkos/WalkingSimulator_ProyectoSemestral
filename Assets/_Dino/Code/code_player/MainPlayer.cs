using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cod.dino;
using Lean.Touch;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace cod.dino
{
    public class MainPlayer : MonoBehaviour
    {
        public enum playerInteractions
        {
            WallNotes,
            NoInteracting,
            Memories,
            OnPause
        }
        public playerInteractions stateInteractions;
        

        [Header("UI Pointer")] 
        [SerializeField] private Image pointer;
        
        private Mouse mouse;
        private PlayerCamera playercamera;
        private PlayerMovement movement;
        private void Start()
        {
            Prepare();
        }
        private void Update()
        {
            if (stateInteractions == playerInteractions.WallNotes)
            {
                PlayerIsInteractingWithWall();
            }

            if (stateInteractions == playerInteractions.NoInteracting)
            {
                playercamera.state = PlayerCamera.cameraState.Moving;
                movement.state = PlayerMovement.playerState.Moving;
                pointer.gameObject.SetActive(true);
            }
            if (stateInteractions == playerInteractions.Memories)
            {
                PlayerIsInteractingWithText();
            }
            if (stateInteractions == playerInteractions.OnPause)
            {
                MakePlayerStatic();
            }
        }
        void Prepare()
        {
            #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
            mouse = Mouse.current;
            #endif
            playercamera = GetComponent<PlayerCamera>();
            movement = GetComponent<PlayerMovement>();
            stateInteractions = playerInteractions.NoInteracting;
        }
        public void PlayerIsInteractingWithWall()
        {
            MakePlayerStatic();
            if (mouse.rightButton.wasPressedThisFrame)
            {
                stateInteractions = playerInteractions.NoInteracting;
            }
        }
        public void PlayerIsInteractingWithText()
        {
            MakePlayerStatic();
        }
        private void MakePlayerStatic()
        {
            playercamera.state = PlayerCamera.cameraState.Static;
            movement.state = PlayerMovement.playerState.Static;
        }
        public void OnPause()
        {
            stateInteractions = playerInteractions.OnPause;
        }
        public void NoPause()
        {
            stateInteractions = playerInteractions.NoInteracting;
        }
    }
}
