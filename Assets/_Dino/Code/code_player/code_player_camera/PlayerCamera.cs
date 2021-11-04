using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using Lean.Touch.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace cod.dino
{
public class PlayerCamera : MonoBehaviour
{
    public cameraState state;
    public enum cameraState
    {
        Static, Moving
    }
    Mouse mouse;
    Camera myCamera;
    float rotationLimit = 0f;
    float rotationX = 0f;
    private float dephcamera;
    private float fieldofview;
    private LeanDragTranslate lean;
    private NotesBehave notesBehave;
    private NotesBehave saveNotesBehave;


    [Header("Player")]
    [SerializeField] private Transform player;

    [SerializeField] private MainPlayer mainPlayer;
    [Header("Camera")]
    [SerializeField] FloatVariables speedCamera;
    [SerializeField] BoolVariables invertedYBool;
    [SerializeField] private BoolVariables invertedXBool;
    [SerializeField] private GameObject placeCameraContainer;
    [SerializeField] private Camera mainCamera;
    [Header("Raycast")]
    [Range(0f,3f)]
    [SerializeField]float distanceHit = 1;

    bool active;
    bool invertedYAxis;
    bool invertedXAxis;
    public bool Active 
    {
        get{return active;}
        set{ active = value;}
    }
    
    void Start()
    {
        Prepare();
    }
    
    void Update()
    {
        if(Active)
        {
            if(mouse!=null && myCamera != null) 
            {
                if (state == cameraState.Moving)
                {
                    CheckMouseInput();
                    BlockMouse();
                }
                else if(state == cameraState.Static)
                {
                    if (mainPlayer.stateInteractions == MainPlayer.playerInteractions.WallNotes)
                    {
                        if (mouse.leftButton.wasPressedThisFrame)
                        {
                            RaycastFromWallNotes();
                        }
                       else if (!mouse.leftButton.IsPressed())
                        {
                            
                            if (notesBehave != null)
                            {
                                if (!notesBehave.IsinPlaced)
                                {
                                    notesBehave.state = NotesBehave.NoteState.Idle;
                                }
                            }
                            
                            if (lean!=null)
                            {
                                notesBehave.LeanCatchRay = false;
                                lean.CanDrag = false;
                                lean = null;
                            }
                        }
                        /* 
                        if (notesBehave.state == NotesBehave.NoteState.Placed)
                        {
                            lean.CanDrag = false;
                        }*/
                    } 
                    UnblockedMouse();
                }
            }
        }
    }

    void Prepare()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_STANDALONE_LINUX
		    mouse = Mouse.current;
		#endif
        try{myCamera = Camera.main;}
        catch{myCamera = GetComponent<Camera>();}

        dephcamera = mainCamera.depth;
        fieldofview = mainCamera.fieldOfView;

		Active = true;  
    }

    void CheckMouseInput()
    {
        Vector2 mouseMovement = mouse.delta.ReadValue();
        rotationX = mouseMovement.x *speedCamera.value;
        rotationLimit += mouseMovement.y * speedCamera.value;
        rotationLimit = Mathf.Clamp(rotationLimit,-80  ,80f);

        //Check the value on the scriptableObject in Y bool 
        if(invertedYBool != null)
        {
            invertedYAxis = invertedYBool.value;
        }
        //Check the value on the scriptableObject in X bool 
        if (invertedXBool != null)
        {
            invertedXAxis = invertedXBool.value;
        }
        // Y camera no inverted
        if (!invertedYAxis)
            myCamera.transform.localRotation = Quaternion.Euler(rotationLimit * -1, 0, 0);
        // Y camera inverted
        if(invertedYAxis)
            myCamera.transform.localRotation = Quaternion.Euler(rotationLimit * 1,0,0);
        //X camera no inverted
        if(!invertedXAxis)
            player.Rotate(Vector3.up * rotationX);
        //X camera inverted
        if(invertedXAxis)
            player.Rotate(Vector3.up * rotationX * -1);

        //Check Click to interact
        if(mouse.leftButton.wasPressedThisFrame)
        {
            if (mainPlayer.stateInteractions == MainPlayer.playerInteractions.NoInteracting)
            {
                GetViewInfo();
            }
        }
        
    }

    void GetViewInfo()
    {   
            RaycastHit hit;
             Vector2 coordinate = new Vector2 (Screen.width/2,Screen.height/2);
             Ray myRay = myCamera.ScreenPointToRay(coordinate);
             if(Physics.Raycast (myRay, out hit, distanceHit))
             {
                 IUsable usable = hit.transform.GetComponent<IUsable>();
                 if(usable !=null)
                 {
                     usable.UseClick();
                 }
             }
    }

    void RaycastFromWallNotes()
    {
        RaycastHit hit;
        Ray myRay = myCamera.ScreenPointToRay(mouse.position.ReadValue());
        if(Physics.Raycast (myRay, out hit, 100))
        {
            print(hit.transform.name);
            
            IUsable usable = hit.transform.GetComponent<IUsable>();
            if(usable !=null)
            {
                usable.UseClick();
            }

            notesBehave = hit.transform.GetComponent<NotesBehave>();
            //saveNotesBehave = notesBehave;
            {
                if (notesBehave != null)
                {
                    if (hit.transform.GetComponent<LeanDragTranslate>())
                    {
                        notesBehave.LeanCatchRay = true;
                        notesBehave.state = NotesBehave.NoteState.Dragging;
                        lean = hit.transform.GetComponent<LeanDragTranslate>();
                        lean.CanDrag = true;
                    }
                    else if(!hit.transform.GetComponent<LeanDragTranslate>())
                    {
                        if(lean!=null)
                        lean.CanDrag = false;
                    }
                }
            } 
          
            NoteTarget noteTarget = hit.transform.GetComponent<NoteTarget>();
            if (noteTarget != null)
            {
                print("ray choca con note target");
                
                if (saveNotesBehave != null)
                {
                    print("Existe note behave");
                    if(saveNotesBehave.IsinPlaced && noteTarget.HasNote)
                    {
                        //notesBehave.IsinPlaced = false;
                        print("Regresar nota");
                    }
                }
                
            }
            
        }  
        
    }
   private void BlockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnblockedMouse()
    {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
    }

    public void ReturnCamera()
    {
        mainCamera.transform.parent = placeCameraContainer.transform.parent;
        mainCamera.transform.position = placeCameraContainer.transform.position;
        mainCamera.depth = dephcamera;
        mainCamera.fieldOfView = fieldofview;


    }
    
    }
}

