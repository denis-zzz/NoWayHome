using UnityEngine;
using UnityEngine.Events;
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
    public UnityEvent thisEvent;

    public void Use()
    {
        if (quantite > 0)
            thisEvent.Invoke();
    }

    public void DecreaseAmount(int amountToDecrease)
    {
        quantite -= amountToDecrease;
        if (quantite < 0)
        {
            quantite = 0;
        }
    }
}
