using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_LevelManager : MonoBehaviour
{
    //
    // mina, maxa, minb, maxb, minc, maxc, spawncount,
    public int[,] addLevels = {
                                {1, 9, 0, 9, 0, 0, 10},
                                {1, 9, 10, 19, 0, 0, 10},
                                {1, 9, 20, 29, 0, 0, 10},
                                {1, 9, 30, 39, 0, 0, 10},
                                {1, 9, 40, 49, 0, 0, 10},
                                {1, 9, 50, 59, 0, 0, 10},
                                {1, 9, 60, 69, 0, 0, 10},
                                {1, 9, 70, 79, 0, 0, 10},
                                {1, 9, 80, 89, 0, 0, 10},
                                {1, 9, 90, 99, 0, 0, 10},
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

    public string symbol = "ADD";

    public int level = 0;
    public int maxLevel = 5;

    public int minA, minB, maxA, maxB, lcount;


    GameSession gameSession;
    MA_LevelManager ma_LevelManager;
    DeathHandler deathHandler;

    private void Awake()
    {
        ma_LevelManager = FindObjectOfType<MA_LevelManager>();
        deathHandler = FindObjectOfType<DeathHandler>();

        symbol = PlayerPrefs.GetString("MathMode");

        if (ma_LevelManager.symbol == "ADD")
        {
            deathHandler.gameName = "MathAddAttack";
        }
        else if (ma_LevelManager.symbol == "SUB")
        {
            deathHandler.gameName = "MathSubAttack";
        }
        else if (ma_LevelManager.symbol == "MUL")
        {
            deathHandler.gameName = "MathMulAttack";
        }
        else if (ma_LevelManager.symbol == "DIV")
        {
            deathHandler.gameName = "MathDivAttack";
        }
        else if (ma_LevelManager.symbol == "MIX")
        {
            deathHandler.gameName = "MathMixAttack";
        }

        print("MA_LevelManager = " + deathHandler.gameName);
    }

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        if (!PlayerPrefs.HasKey(symbol + "Level"))
        {
            PlayerPrefs.SetInt(symbol + "Level", 0);
        }

        level = PlayerPrefs.GetInt(symbol + "Level");

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
            print("MA_LevelManager Score = " + score + " " + i);
        }

        gameSession.addToScore(score);
    }

    public void RunLevels(int val)
    {
        PlayerPrefs.SetInt(symbol + "Level", level);



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