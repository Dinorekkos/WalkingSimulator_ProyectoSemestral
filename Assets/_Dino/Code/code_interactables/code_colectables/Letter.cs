using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using cod.dino;

public class Letter : MonoBehaviour,IUsable
{
    [SerializeField] private MainPlayer mainPlayer;
    public UnityEvent OnUse;
    public bool CanInteract
    {
        get{return canInteract;}
        set {canInteract = value;}
    }   
    bool canInteract;
    public void UseClick()
    {
        mainPlayer.stateInteractions = MainPlayer.playerInteractions.Memories;
        if(OnUse !=null) 
        {
            OnUse.Invoke();
        }
        
    }
    private void Start()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
    }
}
