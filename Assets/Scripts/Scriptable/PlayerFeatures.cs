using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerFeatures : ScriptableObject
{
    public int compteur_tir;
    public int compteur_stab;
    public int compteur_mort;
    public int compteur_mort_tir;
    public float compteur_immobile;
    public float compteur_sprint;
    public int compteur_interactions;
    public int compteur_dialogue;
    public int compteur_loot;
    public int compteur_trade;
    public int compteur_killer;
    public int compteur_socializer;
    public int compteur_inventaire;
    public int compteur_dialogue_skip;

    public void reset()
    {
        compteur_tir = 0;
        compteur_stab = 0;
        compteur_mort = 0;
        compteur_mort_tir = 0;
        compteur_immobile = 0;
        compteur_sprint = 0;
        compteur_interactions = 0;
        compteur_dialogue = 0;
        compteur_loot = 0;
        compteur_trade = 0;
        compteur_killer = 0;
        compteur_socializer = 0;
        compteur_inventaire = 0;
        compteur_dialogue_skip = 0;
    }
}
