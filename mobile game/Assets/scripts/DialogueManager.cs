using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public Text Name;
    public Text Dialoguetext;

    public Animator animator;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        Name.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }
    IEnumerator typeSentence(string sentence)
    {
        Dialoguetext.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            Dialoguetext.text += letter;
            yield return null;
        }
    }
    public void endDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
