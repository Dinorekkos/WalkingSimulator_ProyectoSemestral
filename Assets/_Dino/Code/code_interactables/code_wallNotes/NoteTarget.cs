using System;
using UnityEngine;

public class NoteTarget : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private NotesBehave noteAttached;
    [SerializeField] private string targetID = "";
    [SerializeField] private bool isValid;

    private bool hasNote;
    private MeshRenderer mesh;
    private Collider collider;

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
}
