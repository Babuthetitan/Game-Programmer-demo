using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonFadeIn : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] float duration = 1;
    [SerializeField] float lerpedValue;

    private void OnEnable()
    {
        StartCoroutine(LerpAlpha(0, 1));
    }

    IEnumerator LerpAlpha(float start, float end)
    {
        var tempColor = image.color;

        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            lerpedValue = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            tempColor.a = lerpedValue;
            image.color = tempColor;

            yield return null;
        }

        lerpedValue = end;
    }
}
