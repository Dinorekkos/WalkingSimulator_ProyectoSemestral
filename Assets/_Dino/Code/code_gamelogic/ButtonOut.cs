using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cod.dino;

public class ButtonOut : MonoBehaviour
{
    [SerializeField] private MainPlayer mainPlayer;
    public void UnlockInputs()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        mainPlayer.stateInteractions = MainPlayer.playerInteractions.NoInteracting;
    }
    
}
