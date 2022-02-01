using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactible
{
    public List<Item> items;
    public bool isOpen;
    private bool isBusy = false;
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
        if (Input.GetKeyDown("space") && playerInRange && !isBusy)
        {
            if (!isOpen)
            {
                interagit.raise();
                StartCoroutine(startOpenChest());
            }
            else if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogText.gameObject.SetActive(false);
                item_received_signal.raise();
            }
        }
    }

    IEnumerator startOpenChest()
    {
        yield return new WaitForEndOfFrame();
        isBusy = true;
        yield return OpenChest();
    }

    IEnumerator OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        anim.SetBool("open", true);

        if (items.Count == 0)
            dialogText.text = "C'est vide !";
        else
        {
            item_signal.raise();
            foreach (Item item in items)
            {
                dialogText.text = item.desc;
                playerInventory.AddItem(item);
                yield return new WaitUntil(() => Input.GetKeyDown("space"));
            }
        }
        isOpen = true;
        isBusy = false;
    }
}
