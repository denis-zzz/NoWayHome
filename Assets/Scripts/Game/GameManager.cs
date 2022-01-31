using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ItemGenerator item_generator;
    public BanditGenerator bandit_generator;
    public Float_Value nombre_bandit;
    private PV_Item bandit_wep;
    private List<PV_Item> liste_armes = new List<PV_Item>();
    public PlayerFeatures features;

    //Web requests
    private static string BASE_URL = "https://rohouens.pythonanywhere.com/api/";
    private bool isInit = false;
    private string result;
    private int joueur_id = 1;

    void Awake()
    {
        give_player_gun();
    }

    public void GenerateBandit()
    {
        List<Transform> spawn_points = player.zone.spawnPoints;
        int nombre = (int)nombre_bandit.runtime_value;
        liste_armes.Add((PV_Item)item_generator.Generate("couteau", 1));
        liste_armes.Add((PV_Item)item_generator.Generate("pistolet", 1));

        bandit_generator.Generate(nombre, liste_armes, spawn_points);
    }

    void Update()
    {
        if (player.state == PlayerState.walk)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SavingSystem.i.Save("saveSlot");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SavingSystem.i.Load("saveSlot");
            }
        }

        if (!isInit)
        {
            //sendDataRequest();
            isInit = true;
        }
    }

    public void increase_cpt_tir()
    {
        features.compteur_tir++;
    }

    public void increase_cpt_stab()
    {
        features.compteur_stab++;
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

    public void increase_cpt_trade()
    {
        features.compteur_trade++;
    }

    public void increase_cpt_killer()
    {
        features.compteur_killer++;
    }

    public void increase_cpt_socializer()
    {
        features.compteur_socializer++;
    }

    public void give_player_gun()
    {
        Item weapon = item_generator.Generate("pistolet", 1);
        player.equiped_weapon = (PV_Item)weapon;
        player.damage.runtime_value = player.equiped_weapon.puissance;
    }

    public void give_player_knife()
    {
        Item weapon = item_generator.Generate("couteau", 1);
        player.equiped_weapon = (PV_Item)weapon;
        player.damage.runtime_value = player.equiped_weapon.puissance;
    }

    public void sendDataRequest()
    {
        string request = BASE_URL + "?add";
        request += "&joueur_id=" + joueur_id.ToString();
        request += "&shotCounter=" + features.compteur_tir;
        request += "&stabCounter=" + features.compteur_stab;
        request += "&deathCounter=" + features.compteur_mort;
        request += "&deathByShotGunCounter=" + features.compteur_mort_tir;
        request += "&sprintTime=" + features.compteur_sprint;
        request += "&killerCounter=" + features.compteur_killer;
        request += "&socializerCounter=" + features.compteur_socializer;
        request += "&freezeTime=" + features.compteur_immobile;
        request += "&dialogCounter=" + features.compteur_dialogue;
        request += "&interactionCounter=" + features.compteur_interactions;
        request += "&tradeCounter=" + features.compteur_trade;
        request += "&lootCounter=" + features.compteur_loot;
        StartCoroutine(GetRequest(request));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            Debug.Log("Request send: " + uri);

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("WebRequest: error");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    result = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + result);
                    break;
            }
        }
    }
}
