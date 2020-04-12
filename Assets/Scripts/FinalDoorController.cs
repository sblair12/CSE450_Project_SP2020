using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalDoorController : MonoBehaviour
{
    public GameObject[] torches;
    public GameObject[] spawners;
    public GameObject exitBoundary;
    public GameObject finalBoundary;
    public GameObject[] finalDoorObjects;
    public float newEnemySpeed;
    public int newEnemyHealth;
    public float endBossDuration;

    public GameObject finalText1;
    public GameObject finalText2;
    public float winDelay;
    public float textShowDuration;
    public float textPauseDuration;
    public float delayBetweenStarts;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckAndLightTorches;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckAndLightTorches;
    }

    public void CheckAndLightTorches(Scene scene, LoadSceneMode mode)
    {
        if (PlayerController.instance != null)
        {
            int torchesLit = PlayerController.instance.torchesLit;

            for (int i = 0; i < torchesLit; i++)
            {
                torches[i].GetComponent<TorchController>().SetLit(true);
            }

            // Check if all torches are lit for endgame
            if (torchesLit == 4)
            {
                // Close the exit and set spawners
                exitBoundary.GetComponent<Boundary>().ToggleEnabled();

                System.Random random = new System.Random();
                List<float> randomFrequencies = new List<float>();
                List<float> randomOffsets = new List<float>();
                for (int i = 0; i < spawners.Length; i++)
                {
                    // Randomize spawner frequencies and offsets so they are unique
                    Spawner spawner = spawners[i].GetComponent<Spawner>();
                    while (spawner.frequency > 6 || randomFrequencies.Contains(spawner.frequency))
                    {
                        spawner.frequency = random.Next(0, 7);
                    }
                    randomFrequencies.Add(spawner.frequency);

                    while (spawner.timeOffset > spawners.Length || randomOffsets.Contains(spawner.timeOffset))
                    {
                        spawner.timeOffset = random.Next(1, spawners.Length + 1);
                    }
                    randomOffsets.Add(spawner.timeOffset);

                    // Make enemies harder
                    EnemyController enemy = spawner.enemyPrefab.GetComponent<EnemyController>();
                    enemy.speed = newEnemySpeed;
                    enemy.health = newEnemyHealth;
                }

                Invoke("OpenFinalDoor", endBossDuration);
            }
        }
    }

    private void OpenFinalDoor()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<Spawner>().isEnabled = false;
        }

        StartCoroutine(StartTextCoroutines());
    }

    private IEnumerator StartTextCoroutines()
    {
        yield return new WaitForSeconds(winDelay);
        StartCoroutine(TextFadeController.TextFade(finalText1, textShowDuration, textPauseDuration, true));
        yield return new WaitForSeconds(delayBetweenStarts);
        StartCoroutine(TextFadeController.TextFade(finalText2, textShowDuration, textPauseDuration, true));
        Invoke("ToggleFinalBoundary", textShowDuration + textPauseDuration);
    }

    private void ToggleFinalBoundary()
    {
        for (int i = 0; i < finalDoorObjects.Length; i++)
        {
            Destroy(finalDoorObjects[i].gameObject);
        }
        finalBoundary.GetComponent<Boundary>().ToggleEnabled();
    }
}
