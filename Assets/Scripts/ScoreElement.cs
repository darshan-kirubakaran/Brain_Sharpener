using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text highscoreText;

    public void NewScoreElement(string _username, int _highscore)
    {
        usernameText.text = _username;
        highscoreText.text = _highscore.ToString();
    }
    
    public void NewScoreElementForTime(string _username, string _highscore)
    {
        usernameText.text = _username;
        highscoreText.text = _highscore.ToString();
    }
}
