using System.Collections;
using System.Collections.Generic;
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

    [Header("Player")]
    [SerializeField] private Transform player;
    [Header("Camera")]
    [SerializeField] FloatVariables speedCamera;
    [Header("Rycast")]
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
    public bool InvertedYAxis
    {
        get{return invertedYAxis;}
        set{invertedYAxis = value;}
    }
    public bool InvertedXAxis
    {
        get{return invertedXAxis;}
        set{invertedYAxis = value;}
    }

    void Start()
    {
        Prepare();
    }

    
    void Update()
    {
        if(active)
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

		active = true;  
    }

    void CheckMouseInput()
    {
        Vector2 mouseMovement = mouse.delta.ReadValue();
        rotationX = mouseMovement.x *speedCamera.value;
        rotationLimit += mouseMovement.y * speedCamera.value;
        rotationLimit = Mathf.Clamp(rotationLimit,-80  ,80f);
    
        if (!invertedYAxis) 
            myCamera.transform.localRotation = Quaternion.Euler(rotationLimit * -1,0,0);
    
       if(invertedYAxis)
            myCamera.transform.localRotation = Quaternion.Euler(rotationLimit * 1,0,0);
        
        if(!invertedXAxis)
            player.Rotate(Vector3.up * rotationX);
        if(invertedXAxis)
            player.Rotate(Vector3.up * rotationX * -1);


        if(mouse.leftButton.wasPressedThisFrame)
        {
            GetViewInfo();
        }
    }

    void GetViewInfo()
    {
        RaycastHit hit;
        Vector2 coordinate = new Vector2 (Screen.width/2,Screen.height/2);
        Ray myRay = myCamera.ScreenPointToRay(coordinate);
        if(Physics.Raycast (myRay, out hit, distanceHit))
        {
            //print (hit.transform.name + "" + hit.point);
            IUsable usable = hit.transform.GetComponent<IUsable>();
            if(usable !=null)
            {
                usable.UseClick();
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
    
}
}

