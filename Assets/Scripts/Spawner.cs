using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeOffset;
    public float frequency;
    public bool isEnabled;

    void Start()
    {
        InvokeRepeating("LaunchEnemy", timeOffset, frequency);
    }

    void LaunchEnemy()
    {
        if (isEnabled)
        {
            enemyPrefab.transform.position = gameObject.transform.position;
            Instantiate(enemyPrefab);
        }
    }
}
