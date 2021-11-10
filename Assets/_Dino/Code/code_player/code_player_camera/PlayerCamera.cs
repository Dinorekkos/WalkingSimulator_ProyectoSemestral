using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
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
    
    Mouse mouse;
    Camera myCamera;
    float rotationLimit = 0f;
    float rotationX = 0f;
    private float dephcamera;
    private float fieldofview;
    private LeanDragTranslate lean;
    private NotesBehave saveNotesBehave;
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
                            if (saveNotesBehave != null)
                            {
                                if (!saveNotesBehave.IsinPlaced)
                                {
                                    saveNotesBehave.state = NotesBehave.NoteState.Idle;
                                }
                            }
                            
                            if (lean!=null)
                            {
                                saveNotesBehave.LeanCatchRay = false;
                                lean.CanDrag = false;
                                lean = null;
                            }
                        }
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
            IUsable usable = hit.transform.GetComponent<IUsable>();
            if(usable !=null)
            {
                usable.UseClick();
            }

           NotesBehave notesBehave = hit.transform.GetComponent<NotesBehave>();
           if (notesBehave != null)
           {
               if (!hit.transform.GetComponent<NotesBehave>())
               {
                   saveNotesBehave = notesBehave;
               }
               saveNotesBehave = hit.transform.GetComponent<NotesBehave>();
               
               if (hit.transform.GetComponent<LeanDragTranslate>())
                    {
                        saveNotesBehave.LeanCatchRay = true;
                        saveNotesBehave.state = NotesBehave.NoteState.Dragging;
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

