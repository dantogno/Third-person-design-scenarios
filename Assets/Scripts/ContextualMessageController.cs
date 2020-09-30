﻿using System.Collections;
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

        StartCoroutine(ShowText("TESTING!", 3));
    }

    private IEnumerator ShowText(string message, float duration)
    {
        messageText.text = message;
        canvasGroup.alpha = 1;
        float fadeStartTime;
        float elapsedTime = 0;
        yield return new WaitForSeconds(duration);
        fadeStartTime = Time.time;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - fadeStartTime;
            canvasGroup.alpha = 1 - elapsedTime / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
