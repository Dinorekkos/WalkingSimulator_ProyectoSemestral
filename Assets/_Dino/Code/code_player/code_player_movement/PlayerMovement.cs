using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace cod.dino
{  
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private MainPlayer mainPlayer;
        public playerState state;
        public enum playerState
        {
             Moving, Static,
        }

        #region Get&Set
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        #endregion

        Keyboard keyBoard;
        Gamepad gamePad;
        CharacterController characterController;

        private bool active;
        private Vector3 movementDirection;
        private Vector3 velocity;
        private Vector3 CamR;
        private Vector3 CamF;
        private float verticalSpeed;
        public float movementSpeed = 1.0f;
        public float gravity = 9.8f;

        Player_AnimationController animationController;
    

        void Prepare()
        {
            #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_EDITOR
            keyBoard = Keyboard.current;
            #endif
            gamePad = Gamepad.current;
            characterController = GetComponent<CharacterController>();
            active = true;
            animationController = GetComponent<Player_AnimationController>();
            mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        }

        public void Start()
        {
            Prepare();
        }
        void FixedUpdate()
        {
            if (active)
            {
                    if (keyBoard != null)
                    {
                        if (state == playerState.Moving)
                        {
                            CheckInputKeyBoard();
                        } 
                        else if(state == playerState.Static)
                        {
                            animationController.IdleAnim();
                        }
                    }
                   
            }
            
        }


        void CheckInputKeyBoard()
        {
            movementDirection = Vector3.zero;
            CamF = Camera.main.transform.forward;
            CamR = Camera.main.transform.right;
            CamF.y = 0;
            CamR.y = 0;
            CamF = CamF.normalized;
            CamR = CamR.normalized;
            
            if(keyBoard.anyKey.isPressed)
            {
                
                if (keyBoard.wKey.isPressed)
                {
                    movementDirection +=CamF;
                    animationController.FrontWalking();
                }
            
                if (keyBoard.sKey.isPressed)
                {
                    movementDirection -= CamF; 
                    animationController.BackwardWalking();    
                }
                
                if (keyBoard.aKey.isPressed)
                {
                    movementDirection -= CamR;
                    animationController.LeftWalking();
                }
                
                if (keyBoard.dKey.isPressed)
                {
                    movementDirection += CamR;
                    animationController.RightWalking();
                }
            }
            else
            {
                animationController.IdleAnim();
            }

            
        
                Gravity();
                movementDirection.Normalize();
                Move(movementDirection);
        }


        void Move(Vector3 direction)
        {
            characterController.Move((direction * 0.1f) * movementSpeed);
        }

        void Gravity()
        {
            movementDirection.y = gravity * Time.deltaTime;
        }
    }

}


