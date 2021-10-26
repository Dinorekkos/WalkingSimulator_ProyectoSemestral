using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using cod.dino;

public class Letter : MonoBehaviour,IUsable
{
    //[SerializeField] private MyLetters myLetters;
    
    [SerializeField] private string letterID;
    [SerializeField] private bool readLetter;
    [SerializeField] private MainPlayer mainPlayer;
    private gamelogic_CountMemories countMemories;
    private int memorie = 1;
    public UnityEvent OnUse;

    public bool ReadLetter
    {
        get { return readLetter; }
        set { readLetter = value; }
    }
    public string LetterID
    {
        get { return letterID; }
    }
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
        MemoryTook();
        
    }
    private void Start()
    {
        countMemories = GameObject.FindGameObjectWithTag("Gamelogic").GetComponent<gamelogic_CountMemories>();
        mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayer>();
        ReadLetter = false;
    }

    public void MemoryTook()
    {
        ReadLetter= true;
        
    }
}
