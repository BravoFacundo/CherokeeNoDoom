using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutAfterTime : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private float fadeTime = 3f;

    void Start()
    {
        Invoke(nameof(StartFade), waitTime);
    }

    void StartFade() => StartCoroutine(Fade());
    IEnumerator Fade()
    {
        Color startColor = text.color;

        float time = 0f;
        while (time < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeTime);

            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }
        text.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
