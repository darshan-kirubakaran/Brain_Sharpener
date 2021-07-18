using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int score = 0;

    public TextMeshProUGUI ScoreTextBox;

    // Start is called before the first frame update
    void Start()
    {
        if(ScoreTextBox != null)
        {
            ScoreTextBox.text = score.ToString();
        }
    }

    public void addToScore(int numToAddToScore)
    {
        print("Score in GameSession = " + score);
        score += numToAddToScore;
        if(ScoreTextBox != null)
        {
            ScoreTextBox.text = score.ToString();
        }
        print("Score: " + score);
    }

    public int ReturnScore()
    {
        return score;
    }
}
