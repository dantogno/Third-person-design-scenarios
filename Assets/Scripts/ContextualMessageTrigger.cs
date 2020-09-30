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

    public delegate void ContextualMessageTriggeredAction(string message, float duration);

    public static event ContextualMessageTriggeredAction ContextualMessageTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (ContextualMessageTriggered != null)
            ContextualMessageTriggered.Invoke(message, messageDuration);
    }
}