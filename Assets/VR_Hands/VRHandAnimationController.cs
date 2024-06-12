using System;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO? Make this event based

public class VRHandAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionReference selectValueReference;
    [SerializeField] private InputActionReference activateValueReference;
    
    private void Update()
    {
        animator.SetFloat("Select", selectValueReference.action.ReadValue<float>());
        animator.SetFloat("Activate",activateValueReference.action.ReadValue<float>() );
    }
}
