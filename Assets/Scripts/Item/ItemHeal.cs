using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : MonoBehaviour
{
    public Float_Value playerHealth;
    public SignalSender health_signal;

    public void Use(int amountToIncrease)
    {
        playerHealth.runtime_value += amountToIncrease;
        health_signal.raise();

    }
}
