using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using cod.dino;

public class Letter : MonoBehaviour,IUsable
{
    [SerializeField] PlayerCamera camera;
    [SerializeField] PlayerMovement movement;
    public UnityEvent OnUse;
    public bool CanInteract
    {
        get{return canInteract;}
        set {canInteract = value;}

    }   
    bool canInteract;
    public void UseClick()
    {
        camera.state = PlayerCamera.cameraState.Static;
        movement.state = PlayerMovement.playerState.Static;
        if(OnUse !=null) 
        {
            OnUse.Invoke();
            
        }
        

    }

}
