using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    private void Update()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseButton()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ChangeGameISPaused(bool gameIsPaused)
    {
        GameIsPaused = gameIsPaused;
    }
}
