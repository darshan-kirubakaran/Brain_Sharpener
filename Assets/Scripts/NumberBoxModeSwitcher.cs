using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBoxModeSwitcher : MonoBehaviour
{
    public GameObject timeDisplay;

    CubeSpawner cubeSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();

        var stopwatch = timeDisplay.GetComponent<Stopwatch>();
        var timer = timeDisplay.GetComponent<Timer>();

        if (PlayerPrefs.GetInt("NumberBoxMode") == 1)
        {
            print("(mode switch) setting continuous mode as true");
            cubeSpawner.continuousMode = true;
            stopwatch.enabled = false;
            timer.enabled = true;
        }
        else
        {
            print("(mode switch) setting continuous mode as false");
            cubeSpawner.continuousMode = false;
            stopwatch.enabled = true;
            timer.enabled = false;
        }
    }
}
