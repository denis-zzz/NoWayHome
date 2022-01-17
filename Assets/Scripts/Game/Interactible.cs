using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{
    public bool playerInRange;
    public SignalSender interagit;
    public SignalSender dialogue;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerInRange)
        {
            interagit.raise();

            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogText.gameObject.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);

                if (gameObject.CompareTag("Habitant"))
                    dialogue.raise();

                dialogText.gameObject.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger
        && !other.CompareTag("Bandit"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger
        && !other.CompareTag("Bandit"))
        {
            playerInRange = false;
        }
    }
}
