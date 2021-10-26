using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WallCheckIds : MonoBehaviour
{
   [Header("Notes")]
   [SerializeField] private Camera camera;
   [SerializeField] private NoteTarget[] targetsNotes;
   [SerializeField] private GameObject[] gameojects;

   [Header("Audio")]
   [SerializeField] private AudioManagerPuzzle audioManagerPuzzle;

   [SerializeField] private string[] soundsArray;
   
   private Mouse mouse;
   private RaycastHit hit;
   private Ray ray;
   
   public void CheckTargetsID()
   {
      bool allValid = true;
      
      //Checa si hay notas validas
      for (int i = 0; i < targetsNotes.Length; i++)
      {
         if (targetsNotes[i].IsValid == false)
         {
            allValid = false;
            
         }
      }

      //Si todas las notas son validas se puede abrir la puerta
      if (allValid)
      {
         for (int i = 0; i < gameojects.Length ; i++)
         {
            gameojects[i].SetActive(false);
         }
         
         audioManagerPuzzle.PlayArray(soundsArray);
         
      }
   }

   private void Update()
   {
      if (mouse.leftButton.wasPressedThisFrame)
      {
         ray = camera.ScreenPointToRay(mouse.position.ReadValue());
         if (Physics.Raycast(ray.origin, ray.direction, out hit))
         {
            WallCheckIds checker = hit.transform.GetComponent<WallCheckIds>();
            if (checker != null)
            {
               CheckTargetsID();
            }
         }
      }
   }
   private void Start()
   {
      Prepare();
   }
   void Prepare()
   {
      #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
      mouse = Mouse.current;
      #endif
      audioManagerPuzzle = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerPuzzle>();
   }
}
