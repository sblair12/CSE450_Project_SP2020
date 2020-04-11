using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorController : MonoBehaviour
{
    public GameObject[] torches;

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
            for (int i = 0; i < PlayerController.instance.torchesLit; i++)
            {
                torches[i].GetComponent<TorchController>().SetLit(true);
            }
        }
    }
}
