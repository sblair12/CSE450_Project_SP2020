using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextFadeController : MonoBehaviour
{
    private static bool alreadyFaded = false;

    public GameObject tutorialText;
    public GameObject objectiveText;
    public float showDuration;
    public float textPauseDuration;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartTextFade;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartTextFade;
    }

    private void StartTextFade(Scene scene, LoadSceneMode mode)
    {
        if (!alreadyFaded)
        {
            alreadyFaded = true;
            StartCoroutine(StartTextCoroutines());
        }
    }

    private IEnumerator StartTextCoroutines()
    {
        StartCoroutine(TextFade(tutorialText, showDuration, textPauseDuration, true));
        yield return new WaitForSeconds(showDuration + textPauseDuration);
        StartCoroutine(TextFade(objectiveText, showDuration, textPauseDuration, true));
    }

    private IEnumerator TextFade(GameObject textObject, float totalDuration, float waitDuration, bool fadeOut)
    {
        // Text fading based on: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/

        Text text = textObject.GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float halfDuration = totalDuration / 2;

        // Fade in
        while (text.color.a < 1f)
        {
            float alpha = text.color.a + (Time.deltaTime / halfDuration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(waitDuration);

        if (fadeOut)
        {
            // Fade out
            while (text.color.a > 0f)
            {
                float alpha = text.color.a - (Time.deltaTime / halfDuration);
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
                yield return null;
            }
        }
    }
}
