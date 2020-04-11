using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance;

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

    private GameObject pausePanel;
    private GameObject gameOverObject;

    private static GameObject canvasClone;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void InstantiateCanvas()
    {
        if (!canvasClone)
        {
            canvasClone = Instantiate(canvasPrefab);
            pausePanel = canvasClone.transform.GetChild(0).gameObject;
            gameOverObject = canvasClone.transform.GetChild(1).gameObject;
            Instantiate(eventSystemPrefab);
        }
    }

    public void ToggleMenu(bool isPaused)
    {
        InstantiateCanvas();

        if (isPaused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        canvasClone.SetActive(true);
        Time.timeScale = 0;
    }

    private void Hide()
    {
        canvasClone.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        InstantiateCanvas();
        canvasClone.SetActive(true);
        pausePanel.SetActive(false);
        gameOverObject.SetActive(true);
    }
}
