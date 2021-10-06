using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamelogic_CountMemories : MonoBehaviour
{
    [SerializeField] private GameObject noteGO;
    [SerializeField] Letter letter;
    [SerializeField] private NotesBehave note;

    [SerializeField] private Letter myletter;
    
    void Update()
    {
        if (note.noteID == letter.LetterID && letter.ReadLetter && myletter.ReadLetter)
        {
            noteGO.SetActive(true);
        }
    }
}
