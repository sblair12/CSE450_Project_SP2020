using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance;

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

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

    public void ToggleMenu(bool isPaused)
    {
        if (!canvasClone)
        {
            canvasClone = Instantiate(canvasPrefab);
            Instantiate(eventSystemPrefab);
        }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
