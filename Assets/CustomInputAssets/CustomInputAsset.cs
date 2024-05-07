using System;
using UnityEngine;

public class CustomInputAsset<T> : ScriptableObject
{
    private T value;
    
    public Action<T> ValueChangedAction;
    
    public T Read()
    {
        return value;
    }

    public void Write(T val)
    {
        if (!Equals(val, value))
        {
            this.value = val;
            ValueChangedAction.Invoke(this.value);   
        }
    }
}
