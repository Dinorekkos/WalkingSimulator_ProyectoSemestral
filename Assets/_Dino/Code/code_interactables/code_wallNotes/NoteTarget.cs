using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTarget : MonoBehaviour
{
    [SerializeField] private string targetID = "";
    private bool isValid;
    private bool hasNote;

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
    }
}
