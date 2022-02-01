using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        Item found_item = items.FirstOrDefault(i => i.nom == item.nom);
        if (found_item == null)
        {
            items.Add(Instantiate(item));
        }
        else
        {
            items[items.IndexOf(found_item)].quantite += item.quantite;
        }
    }
}