using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 90;
    public TextMeshProUGUI timeText;
    public bool timerIsOver = false;
    public bool canRunTimer = true;
    public AudioClip shortBeep;
    public AudioClip longBeep;

    DeathHandler deathHandler;
    GameSession gameSession;
    CubeSpawner cubeSpawner;

    AudioSource audioSource;

    private void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
        gameSession = FindObjectOfType<GameSession>();
        cubeSpawner = FindObjectOfType<CubeSpawner>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canRunTimer)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                if (timeValue < 10)
                {
                    if (timeValue % 1 == 0)
                    {
                        if (!audioSource.isPlaying) // So it doesn't layer
                        {
                            audioSource.PlayOneShot(shortBeep);
                        }
                    }
                }
            }
            else
            {
                if (!timerIsOver)
                {
                    timeValue = 0;
                    timerIsOver = true;
                    gameSession.addToScore(cubeSpawner.highestNumber);
                    deathHandler.HandleDeathCondition(false);
                }
            }

            if (timeValue < 10)
            {
                timeText.color = Color.red;
            }

            DisplayTime(timeValue);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if(timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        // converting time value to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer(float startTimeForTimer)
    {
        timeValue = startTimeForTimer;
    }

    public void PauseTimer()
    {
        canRunTimer = false;
    }

    public void ResumeTimer()
    {
        canRunTimer = true;
    }
}
