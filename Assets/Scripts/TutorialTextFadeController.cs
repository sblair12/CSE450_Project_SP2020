using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTextFadeController : MonoBehaviour
{
    private static bool alreadyFaded = false;

    public GameObject tutorialText;
    public GameObject objectiveText;
    public GameObject[] boundaries;
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
        ToggleBoundaries();
        StartCoroutine(TextFadeController.TextFade(tutorialText, showDuration, textPauseDuration, true));
        yield return new WaitForSeconds(showDuration + textPauseDuration);
        StartCoroutine(TextFadeController.TextFade(objectiveText, showDuration, textPauseDuration, true));
        Invoke("ToggleBoundaries", showDuration + textPauseDuration);
    }

    private void ToggleBoundaries()
    {
        // Toggle boundaries
        foreach (GameObject boundary in boundaries)
        {
            boundary.GetComponent<Boundary>().ToggleEnabled();
        }
    }
}
