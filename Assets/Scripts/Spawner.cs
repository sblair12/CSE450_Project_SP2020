using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeOffset;
    public float frequency;

    void Start()
    {
        InvokeRepeating("LaunchEnemy", timeOffset, frequency);
    }

    void LaunchEnemy()
    {
        enemyPrefab.transform.position = gameObject.transform.position;
        Instantiate(enemyPrefab);
    }
}
