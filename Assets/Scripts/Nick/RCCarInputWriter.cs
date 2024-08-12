using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class RCCarInputWriter : MonoBehaviour
{
    public InputMode InputMode;

    public Vector2InputAsset InputAsset;
    
    [Header("XR Controller Inputs")]
    public InputActionReference XRAccelerateAction;
    public InputActionReference XRBrakeAction;
    public InputActionReference XRSteerAction;
    
    [Header("Interactable Inputs")]
    public FloatInputAsset InteractableAccelerationValue;
    public FloatInputAsset InteractableSteerValue;
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
        InputAsset.Write(new Vector2(InteractableSteerValue.Read(), InteractableAccelerationValue.Read()));
    }
}

public enum InputMode
{
    keyboard,
    XRController,
    Interactables
}
