using System;
using UnityEngine;
using UnityEngine.Events;
public class NoteTarget : MonoBehaviour,IUsable
{

    [SerializeField] private GameObject particle;
    [SerializeField] private NotesBehave noteAttached;
    [SerializeField] private string targetID = "";
    [SerializeField] private bool isValid;

    public UnityEvent onUse;
    private NotesBehave note;
    private bool hasNote;
    private MeshRenderer mesh;
    private Collider collider;
    private bool canInteract;

    public bool CanInteract 
    { get; set; }

    public void UseClick()
    {
        if (onUse != null)
        {
            onUse.Invoke();
        }
    }
    public string TargetId
    {
        get { return targetID; }
    }

    public bool IsValid
    {
        get { return isValid;}
        set { isValid = value; }
    }

    public bool HasNote
    {
        get { return hasNote; }
        set { hasNote = value; }
    }

    private void Start()
    {
        IsValid = false;
        mesh = this.GetComponent<MeshRenderer>();
        collider = this.GetComponent<Collider>();
        mesh.enabled = false;
        collider.enabled = false;
        particle.SetActive(false);

    }

    private void Update()
    {
        if (noteAttached.CanInteract)
        {
            mesh.enabled = true;
            collider.enabled = true;
            particle.SetActive(true);
        }
    }

    public void ReturnNoteAttached()
    {
        if (note != null)
        {
             note.state= NotesBehave.NoteState.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        note = other.GetComponent<NotesBehave>();
        if (note == null)
            note = null;
    }
}
