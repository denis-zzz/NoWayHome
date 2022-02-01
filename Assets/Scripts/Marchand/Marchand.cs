using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marchand : HabitantDialog
{
    public Shop shop;
    public IEnumerator Trade()
    {
        yield return shop.startTrading(this);
    }

    public override void Update()
    {
        if (Input.GetKeyDown("space") && playerInRange)
        {
            if (!inDialog)
            {
                StartCoroutine(Trade());
                interagit.raise();
            }
        }
    }
}
