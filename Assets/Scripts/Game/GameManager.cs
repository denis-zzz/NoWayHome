using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Bandit bandit;
    public ItemGenerator item_generator;

    void Awake()
    {
        Item beginning_weapon = item_generator.Generate("couteau", 1);
        player.equiped_weapon = (PV_Item)beginning_weapon;
        bandit.equiped_weapon = (PV_Item)beginning_weapon;
    }


}
