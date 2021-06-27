using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_Checker : MonoBehaviour
{
    Spawner spawner;
    GameSession gameSession;
    DeathHandler deathHandler;
    MA_ChoiceManager ma_ChoiceManager;
    MA_LevelManager ma_LevelManager;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        gameSession = FindObjectOfType<GameSession>();
        deathHandler = FindObjectOfType<DeathHandler>();
        ma_ChoiceManager = FindObjectOfType<MA_ChoiceManager>();
        ma_LevelManager = FindObjectOfType<MA_LevelManager>();
    }

    public void CheckMathAnswer(string value)
    {
        print("BlockCout = " + spawner.BlockQueue.Count.ToString());

        if (spawner.BlockQueue.Count > 0)
        {
            var oldBlock = spawner.BlockQueue.Dequeue();
            print("Value From Deqeued Block = " + oldBlock);

            if (oldBlock.name.ToLower() == value.ToLower())
            {
                // Right

                if(spawner.BlockQueue.Count > 0)
                {
                    ma_ChoiceManager.ChoiceValueChanger();
                }

                spawner.audioSource.PlayOneShot(spawner.righClickSound);
                gameSession.addToScore(1);
                Destroy(oldBlock);
            }
            else
            {
                // Wrong
                //deathHandler = FindObjectOfType<DeathHandler>();
                Handheld.Vibrate();
                PlayerPrefs.SetInt("MA_CurrentLevel", ma_LevelManager.level);
                print("DeathHandler = " + deathHandler);
                deathHandler.HandleDeathCondition(false);
                print("you Lost");
                Destroy(oldBlock);
            }
        }
    }
}
