using System;
using UnityEngine.Events;
using UnityEngine;
using cod.dino;
using  UnityEngine.InputSystem;

public class WallNotes : MonoBehaviour, IUsable
{
    [Header("States")] 
    public wallState state;
    public enum wallState
    {
        onUse, onWaiting
    }
    
    [Header("Cameras")]
    [SerializeField] public GameObject cameraAttachedToWall;
    [SerializeField] public GameObject cameraPlayer;
    
    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform placetomove;

    [Header("Memories")] 
    [SerializeField] private int memoriesOfWall;
    
    
    public UnityEvent OnUse;
    
    private MainPlayer main;
    private Mouse mouse;

    public bool CanInteract
    {
        get{return canInteract;}
        set {canInteract = value;}
    }   
    
    bool canInteract;
    public void UseClick()
    {
        state = wallState.onUse;
        main = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        main.stateInteractions = MainPlayer.playerInteractions.WallNotes;
        
        if(OnUse !=null) 
        {
            OnUse.Invoke();
        }
    }
    public void ActivateMainCamera()
    {
        if (cameraAttachedToWall.activeInHierarchy && !cameraPlayer.activeInHierarchy)
        { 
            cameraPlayer.SetActive(true);
            cameraAttachedToWall.SetActive(false);
        }
    }

    private void Update()
    {
        if (mouse.rightButton.wasPressedThisFrame)
        {
            if (main.stateInteractions == MainPlayer.playerInteractions.NoInteracting)
            {
                //print("Salir de wallnote");
                state = wallState.onWaiting;
                try
                {
                    ActivateMainCamera();
                }
                catch
                {
                    print("No se pudo activar main camera");
                }
            }
        }
        if (main.stateInteractions == MainPlayer.playerInteractions.WallNotes)
        {
            playerTransform.position = placetomove.position;
        }
    }

    private void Start()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
        mouse = Mouse.current;
        #endif

        this.GetComponent<WallNotes>().enabled = false;
        state = wallState.onWaiting;
    }
}
