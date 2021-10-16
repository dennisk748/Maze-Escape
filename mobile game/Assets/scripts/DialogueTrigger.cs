using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnCollisionEnter(Collision collision)
    {
        dialogueTrigger();
    }

    public void dialogueTrigger()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
}
