using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image bloodImage;
    public float fadeDuration = 1.0f;

    private bool isFading = false;

    void Update()
    {
        if (isFading)
        {
            // Gradually increase the alpha channel of the image
            Color color = bloodImage.color;
            color.a += Time.deltaTime / fadeDuration;
            bloodImage.color = color;

            // If the alpha channel reaches 1, stop the fading
            if (color.a >= 1)
            {
                color.a = 1;
                bloodImage.color = color;
                isFading = false;
            }
        }
    }

    public void ShowBloodEffect()
    {
        // Start showing the blood image
        if (bloodImage != null)
        {
            bloodImage.gameObject.SetActive(true);
            isFading = true;
        }
    }

    public void HideBloodEffect()
    {
        // Start hiding the blood image
        if (bloodImage != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        // Gradually decrease the alpha channel of the image
        Color color = bloodImage.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime / fadeDuration;
            bloodImage.color = color;
            yield return null;

        }
    }
}
