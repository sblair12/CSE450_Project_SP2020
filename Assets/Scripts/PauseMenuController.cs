using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance;

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;
    public float winScreenDelay;
    public float loseScreenDelay;
    public string nameOfWinScene;

    private GameObject pausePanel;
    private GameObject endTextObject;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckEndGame;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckEndGame;
    }

    private void InstantiateCanvas()
    {
        if (!canvasClone)
        {
            canvasClone = Instantiate(canvasPrefab);
            pausePanel = canvasClone.transform.GetChild(0).gameObject;
            endTextObject = canvasClone.transform.GetChild(1).gameObject;
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

    private void CheckEndGame(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == nameOfWinScene)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool win)
    {
        InstantiateCanvas();
        canvasClone.SetActive(true);
        pausePanel.SetActive(false);

        Text text = endTextObject.GetComponent<Text>();
        if (win)
        {
            text.text = "YOU WIN";
            text.color = new Color(0, 0, 0.50f);
            Invoke("ResetGame", winScreenDelay);
        }
        else
        {
            text.text = "GAME OVER";
            text.color = new Color(0.70f, 0, 0);
            Invoke("ResetGame", loseScreenDelay);
        }
        endTextObject.SetActive(true);
    }

    private void ResetGame()
    {
        PlayerController.instance.LoadMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
