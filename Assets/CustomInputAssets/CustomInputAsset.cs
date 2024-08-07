using System;
using UnityEngine;

public class CustomInputAsset<T> : ScriptableObject
{
    protected T value;

    public Action<T> ValueChangedAction;

    public bool LogValueOnValueChanged;
    
    public T Read()
    {
        return value;
    }

    protected void Write(T val)
    {
        //move to if statement below once writing is fixed
        if(LogValueOnValueChanged) Debug.Log(val);
        
        if (!Equals(val, value))
        {
            this.value = val;
        }
    }
}
