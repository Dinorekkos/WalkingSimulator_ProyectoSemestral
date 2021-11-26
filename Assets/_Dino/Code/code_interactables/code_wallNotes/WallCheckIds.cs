using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WallCheckIds : MonoBehaviour
{
   [Header("Event")] 
   public UnityEvent onUse;
   
   [Header("WallNotes")]
   [SerializeField] private WallNotes wallNotes;

   [Header("Notes")]
   [SerializeField] private Camera camera;
   [SerializeField] public NoteTarget[] targetsNotes;

   [Header("Audio")]
   [SerializeField] private AudioManagerPuzzle audioManagerPuzzle;
   [SerializeField] private string clipName;

   [SerializeField] private ShakeEffect shakeEffect;
   
   private Mouse mouse;
   private RaycastHit hit;
   private Ray ray;
    bool allValid = false;
   
   public void CheckTargetsID()
   {
      allValid = true;
      for (int i = 0; i < targetsNotes.Length; i++)
      {
         if (targetsNotes[i].IsValid == false)
         {
            allValid = false;
         }
      }
      if (!allValid)
      {
          shakeEffect.DOShake();
          audioManagerPuzzle.Play("DoorLock");
      }
   }

   void OpenDoor()
   {
      if (allValid)
      {
         shakeEffect.DORotate();
         onUse.Invoke();
         audioManagerPuzzle.Play(clipName);
      }
   }

   public void DeactivateNotes()
   {
      for (int i = 0; i < targetsNotes.Length; i++)
      {
         //targetsNotes[i].gameObject.GetComponent<NotesBehave>().enabled = false;
         //targetsNotes[i].gameObject.GetComponent<LeanDragTranslate>().enabled = false;
      }
   }

   private void Update()
   {
      if (mouse.leftButton.wasPressedThisFrame && wallNotes.state == WallNotes.wallState.onUse)
      {
         ray = camera.ScreenPointToRay(mouse.position.ReadValue());
         if (Physics.Raycast(ray.origin, ray.direction, out hit))
         {
            WallCheckIds checker = hit.transform.GetComponent<WallCheckIds>();
            if (checker != null)
            {
               CheckTargetsID();
               OpenDoor();
               
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
