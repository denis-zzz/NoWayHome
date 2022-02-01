using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCouteau : MonoBehaviour
{
    public SignalSender knife_signal;
    public void Use()
    {
        knife_signal.raise();
    }
}
