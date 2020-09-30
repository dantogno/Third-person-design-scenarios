using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 1;

    private TMP_Text messageText;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        messageText = GetComponent<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private IEnumerator ShowText(string message, float duration)
    {
        // show the message for the duration
        messageText.text = message;
        canvasGroup.alpha = 1;
        float fadeStartTime;
        float elapsedTime = 0;
        yield return new WaitForSeconds(duration);

        // then fade out
        fadeStartTime = Time.time;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - fadeStartTime;
            canvasGroup.alpha = 1 - elapsedTime / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    private void OnContextualMessageTriggered(string message, float duration)
    {
        // for a robust system, we would want to set up some sort of queue system.
        // For now, we'll just stop the current coroutine before starting a new one.
        StopAllCoroutines();
        StartCoroutine(ShowText(message, duration));
    }

    private void OnEnable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered += OnContextualMessageTriggered;
    }
    private void OnDisable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered -= OnContextualMessageTriggered;
    }
}
