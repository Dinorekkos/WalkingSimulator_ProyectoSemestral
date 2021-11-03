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
    [SerializeField] public Camera cameraAttachedToWall;
    [SerializeField] public Camera cameraPlayer;

    [SerializeField] public GameObject cameraContainer;
    //[SerializeField]private Camera componentCamera;
    
    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform placetomove;
    [SerializeField] private PlayerCamera playerCameraScript;

    public UnityEvent OnUse;
    public bool interact;
    
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
        interact = true;
        state = wallState.onUse;
        main = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        main.stateInteractions = MainPlayer.playerInteractions.WallNotes;

        if (OnUse != null)
        {
            
            OnUse.Invoke();
            
        }
    }

    public void MoveMainCamera()
    {



        cameraPlayer.transform.position = cameraContainer.transform.position;
        cameraPlayer.transform.rotation = cameraContainer.transform.rotation;
        cameraPlayer.fieldOfView = cameraAttachedToWall.fieldOfView;
        cameraPlayer.depth = cameraAttachedToWall.depth;
    }
    public void ActivateMainCamera()
    {
        /*if (cameraAttachedToWall.activeInHierarchy && !cameraPlayer.activeInHierarchy)
        { 
            cameraPlayer.SetActive(true);
            cameraAttachedToWall.SetActive(false);
            
        }*/
    }

    private void Update()
    {
        if (mouse.rightButton.wasPressedThisFrame)
        {
            if (main.stateInteractions == MainPlayer.playerInteractions.NoInteracting)
            {
                interact = false;
                state = wallState.onWaiting;
                
                try
                {
                    ActivateMainCamera();
                    playerCameraScript.ReturnCamera();
                }
                catch
                {
                    print("No se pudo activar main camera");
                }
                
            }
        }
        if (state == wallState.onUse)
        {
            //componentCamera.enabled = true;
            print("interactuando con este wall notes" );
            playerTransform.position = placetomove.position;
        }
        else
        {
            //componentCamera.enabled = false;
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
