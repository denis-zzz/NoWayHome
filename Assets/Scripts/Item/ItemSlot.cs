using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    public Text itemName;
    public Item item;
    public InventoryManager manager;

    public void Setup(Item newItem, InventoryManager newManager)
    {
        item = newItem;
        manager = newManager;
        if (item)
        {
            itemName.text = item.item_type.ToString();
        }
    }

    public void ClickedOn()
    {
        if (item)
        {
            manager.SetupDescriptionAndButton(item.desc,
                (item.item_type == ItemType.soin), item);
        }
    }

}
