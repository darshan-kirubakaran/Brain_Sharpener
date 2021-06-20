using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_Checker : MonoBehaviour
{
    Spawner spawner;
    GameSession gameSession;
    DeathHandler deathHandler;
    MA_ChoiceManager ma_ChoiceManager;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        gameSession = FindObjectOfType<GameSession>();
        deathHandler = FindObjectOfType<DeathHandler>();
        ma_ChoiceManager = FindObjectOfType<MA_ChoiceManager>();
    }

    public void CheckMathAnswer(string value)
    {

        if (spawner.BlockQueue.Count > 0)
        {
            var oldBlock = spawner.BlockQueue.Dequeue();

            if (oldBlock.name.ToLower() == value.ToLower())
            {
                // Right

                ma_ChoiceManager.ChoiceValueChanger();
                spawner.audioSource.PlayOneShot(spawner.righClickSound);
                gameSession.addToScore(1);
                Destroy(oldBlock);
            }
            else
            {
                // Wrong

                Handheld.Vibrate();
                deathHandler.HandleDeathCondition(false);
                print("you Lost");
                Destroy(oldBlock);
            }
        }
    }
}
