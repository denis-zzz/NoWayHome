using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public IEnumerator startTrading(Marchand merchant)
    {
        int selectedChoice = 0;
        yield return merchant.startDialog(
            choices: new List<string>() { "Acheter", "Vendre", "Quitter" },
            onChoice: choiceIndex => selectedChoice = choiceIndex);

        if (selectedChoice == 0)
        {
            Debug.Log("ACHAT");
        }
        else if (selectedChoice == 1)
        {
            Debug.Log("VENTE");
        }
        else if (selectedChoice == 2)
        {
            yield break;
        }
    }
}
