using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditGenerator : MonoBehaviour
{
    public GameObject banditPrefab;

    public void Generate(int nombre, List<PV_Item> armes, List<Transform> list_transf)
    {
        StartCoroutine(SpawnCo(nombre, armes, list_transf));
    }

    private IEnumerator SpawnCo(int nombre, List<PV_Item> armes, List<Transform> list_transf)
    {
        for (int i = 0; i < nombre; i++)
        {
            Transform transf = list_transf[Random.Range(0, list_transf.Count)];
            GameObject bandit = Instantiate(banditPrefab,
            transf.position, Quaternion.identity);

            bandit.GetComponent<Bandit>().equiped_weapon =
            armes[Random.Range(0, armes.Count)];

            bandit.transform.SetParent(transf.parent);
            yield return new WaitForSeconds(.3f);
        }
    }
}
