using System;
using UnityEngine;
using UnityEngine.UI;
public class Sing : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;

    [SerializeField] private Text dialogText;

    [SerializeField] private string dialog;

    [SerializeField] private bool playerInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
