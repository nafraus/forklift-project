using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class RCCarInputWriter : MonoBehaviour
{
    public InputMode InputMode;

    public Vector2InputAsset InputAsset;

    public InputActionReference XRAccelerateAction;
    public InputActionReference XRBrakeAction;
    public InputActionReference XRSteerAction;
    void Update()
    {
        switch (InputMode)
        {
           case InputMode.keyboard:
               WriteInputFromKeyboard();
               break;
           case InputMode.XRController:
               WriteInputFromXRController();
               break;
           case InputMode.Interactables:
               WriteInputFromInteractables();
               break;
        }
    }

    void WriteInputFromKeyboard()
    {
        InputAsset.Write(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
    void WriteInputFromXRController()
    {
        InputAsset.Write(new Vector2( XRSteerAction.action.ReadValue<Vector2>().x, XRAccelerateAction.action.ReadValue<float>()-XRBrakeAction.action.ReadValue<float>()));
    }
    void WriteInputFromInteractables()
    {
        throw new NotImplementedException();
    }
}

public enum InputMode
{
    keyboard,
    XRController,
    Interactables
}
