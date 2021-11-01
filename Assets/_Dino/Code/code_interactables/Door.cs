using UnityEngine;
using UnityEngine.Events;
using cod.dino;
public class Door : MonoBehaviour, IUsable 
{
    //[SerializeField] AudioManagerPuzzle audioManager;
    //[SerializeField] private string nameClip;
    
    public UnityEvent OnUse;
    public bool CanInteract
    {
        get{return canInteract;}
        set {canInteract = value;}
    }   
    bool canInteract;
    
    public void UseClick()
    {
        if(OnUse !=null)
        {
            OnUse.Invoke();
            //audioManager.Play(nameClip);
        }
    }

    

    


}