using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    #region Singletone

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public Text nameText;
    public Text dialogueText;

    public Animator _animator;

    // массив предложений диалога
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

   public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

   IEnumerator TypeSentence(string sentence)
   {
       dialogueText.text = "";
       foreach (char letter in sentence)
       {
           dialogueText.text += letter;
           yield return null;
       }
   }

   public void EndDialogue()
    {
        _animator.SetBool("isOpen", false);
    }
}
