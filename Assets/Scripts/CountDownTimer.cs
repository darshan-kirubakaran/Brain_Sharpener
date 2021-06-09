using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float minTimeBetweenSpawns = 4f;
    [SerializeField] float maxTimeBetweenSpawns = 6f;

    float startTime = 20;
    float currentTime = 0;

    bool isAllowedToDeacreaseTime = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;


        if (minTimeBetweenSpawns <= 0.5f)
        {
            isAllowedToDeacreaseTime = false;
            minTimeBetweenSpawns = 0.5f;
            maxTimeBetweenSpawns = 1f;
        }
        else if (minTimeBetweenSpawns <= 1f  )
        {
            minTimeBetweenSpawns = 1f;
            maxTimeBetweenSpawns = 2f;
            startTime = 30;
        }

        if (currentTime <= 0)
        {
            if (isAllowedToDeacreaseTime)
            {
                minTimeBetweenSpawns -= 0.5f;
                maxTimeBetweenSpawns -= 0.5f;
            }

            currentTime = startTime;
        }


    }

    public float ReturnMinTimeBetweenSpawns() { return minTimeBetweenSpawns; }

    public float ReturnMaxTimeBetweenSpawns() { return maxTimeBetweenSpawns; }
}
