using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAction, gripAction;
    
    private Animator hand;
    
    void Start()
    {
        hand = GetComponent<Animator>();
    }

    void Update()
    {
        hand.SetFloat("Trigger", pinchAction.action.ReadValue<float>());
        hand.SetFloat("Grip", gripAction.action.ReadValue<float>());
    }

    public float HandGripValue() => gripAction.action.ReadValue<float>();
}
