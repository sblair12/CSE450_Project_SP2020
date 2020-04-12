using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorController : MonoBehaviour
{
    public GameObject[] torches;
    public GameObject[] spawners;
    public GameObject exitBoundary;
    public float newEnemySpeed;
    public int newEnemyHealth;

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
            }
        }
    }
}
