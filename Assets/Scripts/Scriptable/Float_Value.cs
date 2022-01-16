using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Float_Value : ScriptableObject, ISerializationCallbackReceiver
{
    public float initial_value;
    public float runtime_value;

    public void OnAfterDeserialize()
    {
        runtime_value = initial_value;
    }

    public void OnBeforeSerialize()
    {

    }
}
