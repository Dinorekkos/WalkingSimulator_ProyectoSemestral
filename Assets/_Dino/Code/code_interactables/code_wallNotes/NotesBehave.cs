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
    [Header("ID")]
    [SerializeField] public string noteID = "";
    
    Vector3 placedPos;
    private Camera cam;
    private RaycastHit hit;
    private Ray ray;
    Mouse mouse;
    private LeanDragTranslate lean;
    private Vector3 inicialPos; 
    private NoteTarget noteTarget;
    
    private void Start()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
        mouse = Mouse.current;
        #endif
        
        lean = gameObject.GetComponent<LeanDragTranslate>();
        cam = lean.Camera;

        inicialPos = transform.position;
        state = NoteState.Idle;
    }

    private void Update()
    {
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
                        if (this.IsinPlaced && noteTarget.HasNote)
                        {
                            IsinPlaced= false;
                        }
                    }
                    if (hit.transform.GetComponent<LeanDragTranslate>())
                    {
                       // print(hit.transform.name + "da con comp leandrag");
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
            else if(!mouse.leftButton.IsPressed())
            {
                if(!IsinPlaced)
                state = NoteState.Idle;
                
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
           // print("green note" + this.name);
        }
        else if(noteTarget.TargetId != noteID)
        {
            noteTarget.IsValid = false;
            //print("red note" +  this.name);
        }
    }
}
