using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ItemGenerator item_generator;
    public BanditGenerator bandit_generator;
    public Float_Value nombre_bandit;
    private PV_Item bandit_wep;
    private List<PV_Item> liste_armes = new List<PV_Item>();
    public PlayerFeatures features;

    void Awake()
    {
        Item beginning_weapon = item_generator.Generate("couteau", 1);
        player.equiped_weapon = (PV_Item)beginning_weapon;
    }

    public void GenerateBandit()
    {
        List<Transform> spawn_points = player.zone.spawnPoints;
        int nombre = (int)nombre_bandit.runtime_value;
        liste_armes.Add((PV_Item)item_generator.Generate("couteau", 1));
        liste_armes.Add((PV_Item)item_generator.Generate("pistolet", 1));

        bandit_generator.Generate(nombre, liste_armes, spawn_points);
    }

    public void increase_cpt_tir()
    {
        features.compteur_tir++;
    }

    public void increase_cpt_mort()
    {
        features.compteur_mort++;
    }

    public void increase_cpt_mort_tir()
    {
        features.compteur_mort_tir++;
    }

    public void increase_cpt_immobile()
    {
        features.compteur_immobile++;
    }

    public void increase_cpt_sprint()
    {
        features.compteur_sprint++;
    }

    public void increase_cpt_interactions()
    {
        features.compteur_interactions++;
    }

    public void increase_cpt_dialogue()
    {
        features.compteur_dialogue++;
    }

    public void increase_cpt_loot()
    {
        features.compteur_loot++;
    }

    public void increase_cpt_killer()
    {
        features.compteur_killer++;
    }

    public void increase_cpt_socializer()
    {
        features.compteur_socializer++;
    }


}
