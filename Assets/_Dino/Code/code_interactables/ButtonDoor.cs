using UnityEngine;
using UnityEngine.Events;
using cod.dino;
public class ButtonDoor : MonoBehaviour, IUsable 
{
    
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
        }
    }

    


}