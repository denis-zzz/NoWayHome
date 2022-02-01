using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : MonoBehaviour
{
    public Float_Value playerHealth;
    public SignalSender health_signal;
    public Float_Value healPower;
    private float healAmount;

    public void Use()
    {
        healAmount = healPower.runtime_value;
        playerHealth.runtime_value += healAmount;
        health_signal.raise();
    }
}
