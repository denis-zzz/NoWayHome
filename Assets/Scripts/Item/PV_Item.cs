public enum PV_ItemType
{
    pistolet,
    couteau,
    soin
}
public class PV_Item : Item
{
    public PV_ItemType pv_item_type;
    public float puissance;
}
