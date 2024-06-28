using System;
using UnityEngine;

public class CustomInputAsset<T> : ScriptableObject
{
    protected T value;

    public Action<T> ValueChangedAction;
    
    public T Read()
    {
        return value;
    }

    protected void Write(T val)
    {
        if (!Equals(val, value))
        {
            this.value = val;
            if(ValueChangedAction!= null) ValueChangedAction.Invoke(this.value);   
        }
    }
}
