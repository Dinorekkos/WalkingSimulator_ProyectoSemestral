using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cod.dino;

public class ButtonOut : MonoBehaviour
{
    [SerializeField] PlayerCamera camera;
    [SerializeField] PlayerMovement movement;

    public void UnlockInputs()
    {
        
        camera.state = PlayerCamera.cameraState.Moving;
        movement.state = PlayerMovement.playerState.Moving;
    }


}
