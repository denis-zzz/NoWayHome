using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marchand : HabitantDialog
{
    public Shop shop;
    private IEnumerator co;

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
                co = Trade();
                StartCoroutine(co);
                interagit.raise();
            }
        }

        if (inDialog && Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(co);
            endDialog();
            choice_box.endChoice();
            dialog_skip_signal.raise();
        }
    }
}
