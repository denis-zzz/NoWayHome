using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Item currentItem;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject useButton;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            foreach (Item item in playerInventory.items)
            {
                GameObject temp =
                    Instantiate(blankInventorySlot,
                    inventoryContent.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryContent.transform);
                ItemSlot newSlot = temp.GetComponent<ItemSlot>();
                if (newSlot)
                {
                    newSlot.Setup(item, this);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString,
        bool isButtonUsable, Item newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString + ". Quantité " +
        newItem.quantite;
        useButton.SetActive(isButtonUsable);
    }

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
        }
    }

}
