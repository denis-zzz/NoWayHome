using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPistolet : MonoBehaviour
{
    public SignalSender gun_signal;
    public void Use()
    {
        gun_signal.raise();
    }
}
