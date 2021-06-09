using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    //const string DIFFICULTY_KEY = "difficulty";
    const string LIVES = "RespawnLives";


    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const int MIN_LIVES = 0;
    const int MAX_LIVES = 3;

    //const float MIN_DIFFICULTY = 0f;
    //const float MAX_DIFFICULTY = 5f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            Debug.Log("Master volume set to " + volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetLives(int lives)
    {
        if (lives >= MIN_LIVES && lives <= MAX_LIVES)
        {
            Debug.Log("Master volume set to " + lives);
            PlayerPrefs.SetInt(LIVES, lives);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static int GetLives()
    {
        return PlayerPrefs.GetInt(LIVES);
    }

    /*public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty setting is not in range");
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }*/
}
