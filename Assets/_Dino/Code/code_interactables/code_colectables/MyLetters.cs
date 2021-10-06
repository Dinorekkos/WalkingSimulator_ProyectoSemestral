using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLetters : MonoBehaviour
{
    [SerializeField] private string MyletterID;
    [SerializeField] private bool readMy;
    
    
    public bool ReadLetter
    {
        get { return readMy; }
        set { readMy = value; }
    }
    public string MyLetterID
    {
        get { return MyletterID; }
    }

    public void AddMyLetterID()
    {
        ReadLetter = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
