using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Boolean_Value : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initial_value;
    public bool runtime_value;

    public void OnAfterDeserialize()
    {
        runtime_value = initial_value;
    }

    public void OnBeforeSerialize()
    {

    }
}
