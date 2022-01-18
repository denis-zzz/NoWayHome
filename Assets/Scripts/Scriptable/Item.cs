using UnityEngine;
public enum ItemType
{
    argent,
    soin,
    document,
    bandage,
    herbe,
    arme,
    munition,
    ferraille,
    poudre
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string nom;
    public float prix;
    public int quantite;
    public string desc;
    public ItemType item_type;
}