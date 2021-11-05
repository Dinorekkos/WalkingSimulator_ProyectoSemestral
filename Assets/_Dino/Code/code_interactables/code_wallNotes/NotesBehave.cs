using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.InputSystem;
using cod.dino;
using UnityEngine.Events;


public class NotesBehave : MonoBehaviour,IUsable
{
    public UnityEvent onUse;
    public enum NoteState
    {
        Idle, Dragging, Placed
    }

    public NoteState state;
    public MainPlayer main;

    public bool isInPlaced;
    public bool IsinPlaced
    {
        get { return isInPlaced;}
        set { isInPlaced = value; }
    }

    private bool leanCatchRay;
    public bool LeanCatchRay
    {
        get { return leanCatchRay; }
        set { leanCatchRay = value; }
    }

    [Header("Visuals")] 
    [SerializeField] private GameObject particle;
    [Header("ID")]
    [SerializeField] public string noteID = "";

    [Header("Colectables")] 
    [SerializeField] private Letter memorie;
    [SerializeField] private Letter thought;
    
    private bool canInteract;

    public bool CanInteract
    {
        get { return canInteract; }
        set { canInteract = value; }
    }
    Vector3 placedPos;
    private RaycastHit hit;
    private Ray ray;
    private Mouse mouse;
    private NoteTarget noteTarget;
    private Vector3 inicialPos;
    private Collider collider;
    private MeshRenderer mesh;
    private void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        collider = this.GetComponent<Collider>();
        particle.SetActive(false);
        canInteract = false;
        inicialPos = transform.position;
        state = NoteState.Idle;
        mesh.enabled = false;
        collider.enabled = false;
    }

    private void Update()
    {
        if (memorie.ReadLetter && thought.ReadLetter)
        {
            mesh.enabled = true;
            collider.enabled = true;
            canInteract = true;
            particle.SetActive(true);
        }
        if (main.stateInteractions == MainPlayer.playerInteractions.WallNotes)
        {
            if (state == NoteState.Idle)
            {
                transform.position = inicialPos;
            }
            if (state == NoteState.Placed)
            {
                CheckID();
            }
        }
    }

    public void UseClick()
    {
        if(onUse !=null)
        {
            onUse.Invoke();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeTarget"))
        {
            
            noteTarget = other.GetComponent<NoteTarget>();
            noteTarget.HasNote = true;
            state = NoteState.Placed;
            IsinPlaced = true;
            transform.position = other.transform.position;
            LeanDragTranslate leanDrag = gameObject.GetComponent<LeanDragTranslate>();
            leanDrag.CanDrag = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CubeTarget"))
        {
            noteTarget = other.GetComponent<NoteTarget>();
            IsinPlaced = false;
            noteTarget.HasNote = false;
            noteTarget.IsValid = false;
        }
    }
    void CheckID()
    {
        if (noteTarget.TargetId == noteID)
        {
            noteTarget.IsValid = true;
        }
        if(noteTarget.TargetId != noteID)
        {
            noteTarget.IsValid = false;
        }
        if (noteTarget.TargetId == null)
        {
            noteTarget.IsValid = false;
            
        }
        if (!noteTarget.HasNote)
        {
            noteTarget.IsValid = false;
        }
        
    }
}
