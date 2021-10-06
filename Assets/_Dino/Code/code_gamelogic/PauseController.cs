using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace cod.dino
{
    public class PauseController : MonoBehaviour
    {
        private bool isOnPause;
        private Keyboard keyboard;
        private MainPlayer mainPlayer;

        [Header("PauseMenu")] 
        [SerializeField] private GameObject pauseGO;

        public bool IsOnPause
        {
            get { return isOnPause; }
            set { isOnPause = value;}
        }


        void Prepare()
        {
            #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
            keyboard = Keyboard.current;
            #endif
            mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
            IsOnPause = false;
            pauseGO.SetActive(false);
        }
       void Start()
        {
            Prepare();
        }

        
        void Update()
        {
            CallPause();
            if (IsOnPause == false)
            {
                pauseGO.SetActive(false);
            }
            else
            {
                pauseGO.SetActive(true);
            }
        }

        void CallPause()
        {
            if (keyboard.escapeKey.wasPressedThisFrame)
            {
                IsOnPause = true;
                mainPlayer.OnPause();
                //print("Pause is on");
            }
        }

        public void CallOffPause()
        {
            IsOnPause = false;
            mainPlayer.NoPause();
        }
        
    }
}
