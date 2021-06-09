using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public float timeValue = 0;
    public TextMeshProUGUI timeText;
    public bool canRunStopwatch = true;

    DeathHandler deathHandler;

    AudioSource audioSource;

    private void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canRunStopwatch)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        // converting time value to minutes nand seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopStopwatch()
    {
        canRunStopwatch = false;
    }

    public float ReturnTime()
    {
        return timeValue;
    }

    public void PauseStopwatch()
    {
        canRunStopwatch = false;
    }

    public void ResumeStopwatch()
    {
        canRunStopwatch = true;
    }
}
