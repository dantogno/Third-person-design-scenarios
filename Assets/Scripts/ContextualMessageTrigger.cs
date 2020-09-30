using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContextualMessageTrigger : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 5)]
    private string message = "hello";

    [SerializeField]
    private float messageDuration = 2;

    public static event Action<string, float> ContextualMessageTriggered;

    private void OnTriggerEnter(Collider other)
    {
        ContextualMessageTriggered?.Invoke(message, messageDuration);
    }
}