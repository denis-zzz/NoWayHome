using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private Item item;
    private PV_Item pv_item;
    public Float_Value heal_power;
    public Float_Value knife_damage;
    public Float_Value gun_damage;

    public Item Generate(string nom, int nombre)
    {

        switch (nom)
        {
            case "argent":
                item = new Item();
                item.nom = "Argent";
                item.prix = 0;
                item.quantite = nombre;
                item.item_type = ItemType.argent;
                return item;

            case "soin":
                pv_item = new PV_Item();
                pv_item.nom = "Soin";
                pv_item.prix = 10;
                pv_item.quantite = nombre;
                pv_item.item_type = ItemType.soin;
                pv_item.pv_item_type = PV_ItemType.soin;
                pv_item.puissance = heal_power.initial_value;
                return pv_item;

            case "document":
                item = new Item();
                item.nom = "Document";
                item.prix = 0;
                item.quantite = nombre;
                item.item_type = ItemType.document;
                return item;

            case "bandage":
                item = new Item();
                item.nom = "Bandage";
                item.prix = 4;
                item.quantite = nombre;
                item.item_type = ItemType.bandage;
                return item;

            case "herbe":
                item = new Item();
                item.nom = "Herbe";
                item.prix = 4;
                item.quantite = nombre;
                item.item_type = ItemType.herbe;
                return item;

            case "couteau":
                pv_item = new PV_Item();
                pv_item.nom = "Couteau";
                pv_item.prix = 20;
                pv_item.quantite = nombre;
                pv_item.item_type = ItemType.arme;
                pv_item.pv_item_type = PV_ItemType.couteau;
                pv_item.puissance = knife_damage.initial_value;
                return pv_item;

            case "pistolet":
                pv_item = new PV_Item();
                pv_item.nom = "Pistolet";
                pv_item.prix = 30;
                pv_item.quantite = nombre;
                pv_item.item_type = ItemType.arme;
                pv_item.pv_item_type = PV_ItemType.pistolet;
                pv_item.puissance = gun_damage.initial_value;
                return pv_item;

            case "munition":
                item = new Item();
                item.nom = "Munition";
                item.prix = 1;
                item.quantite = nombre;
                item.item_type = ItemType.munition;
                return item;

            case "ferraille":
                item = new Item();
                item.nom = "Ferraille";
                item.prix = 1;
                item.quantite = nombre;
                item.item_type = ItemType.ferraille;
                return item;

            case "poudre":
                item = new Item();
                item.nom = "Poudre";
                item.prix = 1;
                item.quantite = nombre;
                item.item_type = ItemType.poudre;
                return item;
        }
        return null;
    }


}
