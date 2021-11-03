using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.InputSystem;
using cod.dino;

public class NotesBehave : MonoBehaviour
{
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

    [Header("Visuals")] 
    [SerializeField] private GameObject particle;
    [Header("ID")]
    [SerializeField] public string noteID = "";

    [Header("Colectables")] 
    [SerializeField] private Letter memorie;
    [SerializeField] private Letter thought;
    
    [Header("AudioManager")]
    [SerializeField] private AudioManagerPuzzle audioManagerPuzzle;

    [SerializeField] private string clipName;
    
    private bool canInteract;

    public bool CanInteract
    {
        get { return canInteract; }
    }
    
    Vector3 placedPos;
    [SerializeField] private Camera cam;
    private RaycastHit hit;
    private Ray ray;
    private Mouse mouse;
    private LeanDragTranslate lean;
    private Vector3 inicialPos; 
    private NoteTarget noteTarget;
    private Collider collider;
    
    private MeshRenderer mesh;
    private void Start()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
        mouse = Mouse.current;
        #endif
        mesh = this.GetComponent<MeshRenderer>();
        collider = this.GetComponent<Collider>();
        lean = gameObject.GetComponent<LeanDragTranslate>();
        //cam = Camera.main;

        particle.SetActive(false);
        canInteract = false;
        inicialPos = transform.position;
        state = NoteState.Idle;
        mesh.enabled = false;
        collider.enabled = false;
        audioManagerPuzzle = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerPuzzle>();
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
            if (mouse.leftButton.wasPressedThisFrame)
            {
                ray = cam.ScreenPointToRay(mouse.position.ReadValue());
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (hit.transform.GetComponent<NoteTarget>())
                    {
                        audioManagerPuzzle.Play(clipName);
                        if (this.IsinPlaced && noteTarget.HasNote)
                        {
                            IsinPlaced= false;
                        }
                    }
                    if (hit.transform.GetComponent<LeanDragTranslate>())
                    {
                        
                        audioManagerPuzzle.Play(clipName);
                        state = NoteState.Dragging;
                        lean = hit.transform.GetComponent<LeanDragTranslate>();
                        lean.CanDrag = true;
                    }
                    else if (!hit.transform.GetComponent<LeanDragTranslate>())
                    {
                        //print( " NO da con comp leandrag");
                        if(lean !=null)
                        lean.CanDrag = false;
                    }
                }
            }
            else if (!mouse.leftButton.IsPressed())
            {
                if (!IsinPlaced)
                {
                    state = NoteState.Idle;
                }
                if (lean)
                {
                    lean.CanDrag = false;
                    lean = null;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeTarget"))
        {
            //print("Detect Collider Place");
            noteTarget = other.GetComponent<NoteTarget>();
            state = NoteState.Placed;
            IsinPlaced = true;
            noteTarget.HasNote = true;
            transform.position = other.transform.position;
            lean.CanDrag = false;
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
        else if(noteTarget.TargetId != noteID)
        {
            noteTarget.IsValid = false;
        }
        else if (noteTarget.TargetId == null)
        {
            noteTarget.IsValid = false;
            
        }
        
    }
}
