using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

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
        yield return new WaitForSeconds(duration);
        canvasGroup.alpha = 0;
    }
}
