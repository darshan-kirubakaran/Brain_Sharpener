using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public List<int> scores = new List<int>();

    public PlayerData()
    {

    }
    public void AddPlayerData(GameSession gameSession)
    {
        Debug.Log("scre == " + gameSession.score);
        Debug.Log("gameSession == " + gameSession);

        scores.Add(gameSession.score);
    }
}
