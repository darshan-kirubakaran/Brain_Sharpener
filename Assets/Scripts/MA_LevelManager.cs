using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_LevelManager : MonoBehaviour
{
    //
    // type, mina, maxa, minb, maxb, minc, maxc, spawncount,
    public int[,] addLevels = {
                                {0, 9, 0, 9, 0, 0, 10},
                                {0, 9, 10, 19, 0, 0, 10},
                                {0, 9, 20, 29, 0, 0, 10},
                                {0, 9, 30, 39, 0, 0, 10},
                                {0, 9, 40, 49, 0, 0, 10},
                                {0, 9, 50, 59, 0, 0, 10},
                                {0, 9, 60, 69, 0, 0, 10},
                                {0, 9, 70, 79, 0, 0, 10},
                                {0, 9, 80, 89, 0, 0, 10},
                                {0, 9, 90, 99, 0, 0, 10},
                                {10, 19, 0, 9, 0, 0, 10},
                                {20, 29, 10, 19, 0, 0, 10},
                                {30, 39, 20, 29, 0, 0, 10},
                                {40, 49, 30, 39, 0, 0, 10},
                                {50, 59, 40, 49, 0, 0, 10},
                                {60, 69, 50, 59, 0, 0, 10},
                                {70, 79, 60, 69, 0, 0, 10},
                                {80, 89, 70, 79, 0, 0, 10},
                                {90, 99, 80, 89, 0, 0, 10}
                            };

    public int level = 0;
    public int maxLevel = 5;

    public int minA, minB, maxA, maxB, lcount;


    GameSession gameSession;

    public void Start()
    {

    

        gameSession = FindObjectOfType<GameSession>();

        if (!PlayerPrefs.HasKey("MA_CurrentLevel"))
        {
            PlayerPrefs.SetInt("MA_CurrentLevel", 0);
        }

        level = PlayerPrefs.GetInt("MA_CurrentLevel");

        print("Level = " + level);

        minA = addLevels[level, 0];
        maxA = addLevels[level, 1];
        minB = addLevels[level, 2];
        maxB = addLevels[level, 3];
        lcount = addLevels[level, 6];

        int score = 0;

        for (int i = 0; i < level; i++)
        {
            score = score + addLevels[i, 6];
        }

        gameSession.addToScore(score);
    }

    public void RunLevels(int val)
    {
        PlayerPrefs.SetInt("MA_CurrentLevel", level);



        if (level + val < maxLevel)
        {
            level = level + val;
            minA = addLevels[level, 0];
            maxA = addLevels[level, 1];
            minB = addLevels[level, 2];
            maxB = addLevels[level, 3];
            lcount = addLevels[level, 6];
        }
    }
}