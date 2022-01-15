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
public class Item
{
    public string nom;
    public float prix;
    public int quantite;
    public string desc;
    public ItemType item_type;
}
