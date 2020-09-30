using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ShowText(string message, float duration)
    {

    }
}
