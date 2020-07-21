using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueManager dialogueManager;
    
    public Dialogue dialogue;

    private bool _playerInRange;

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    private void Update()
    {
        if (_playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TriggerDialogue();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            _playerInRange = false;
        }
    }
}
