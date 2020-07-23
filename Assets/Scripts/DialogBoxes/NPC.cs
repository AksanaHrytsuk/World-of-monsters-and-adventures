
using UnityEngine;

public class NPC : Sing
{
    public DialogueManager dialogueManager;
    
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dialogBox.activeInHierarchy)
                {
                    dialogBox.SetActive(false);
                }
                else
                {
                    dialogBox.SetActive(true);
                    TriggerDialogue();
                }
            }
        }
    }
}
