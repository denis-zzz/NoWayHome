using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactible
{
    public Item item;
    public bool isOpen;
    public SignalSender item_signal;
    public SignalSender item_received_signal;
    public SignalSender interagit;
    private Animator anim;
    public Inventory playerInventory;
    public GameObject dialogBox;
    public Text dialogText;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && playerInRange)
        {
            if (!isOpen)
            {
                interagit.raise();
                OpenChest();
            }
            else if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogText.gameObject.SetActive(false);
                item_received_signal.raise();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        anim.SetBool("open", true);

        if (item != null)
        {
            dialogText.text = item.desc;
            playerInventory.AddItem(item);
            playerInventory.currentItem = item;
            item_signal.raise();
        }
        else
        {
            dialogText.text = "C'est vide !";
        }

        isOpen = true;
    }
}
