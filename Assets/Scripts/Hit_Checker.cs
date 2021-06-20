using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Checker : MonoBehaviour
{
    Spawner spawner;
    GameSession gameSession;
    DeathHandler deathHandler;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        gameSession = FindObjectOfType<GameSession>();
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    public void CheckTextIfRight(string colorName)
    {
        if (spawner.BlockQueue.Count > 0)
        {
            var oldBlock = spawner.BlockQueue.Dequeue();

            if (oldBlock.GetComponentInChildren<TextMesh>().text.ToLower() == colorName.ToLower())
            {
                // Right

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

    public void CheckColorIfRight(Color color)
    {
        if (spawner.BlockQueue.Count > 0)
        {
            var oldText = spawner.BlockQueue.Dequeue();

            if (oldText.GetComponentInChildren<TextMesh>().color == color)
            {
                // Right

                spawner.audioSource.PlayOneShot(spawner.righClickSound);
                gameSession.addToScore(1);
                Destroy(oldText);
            }
            else
            {
                // Wrong

                Handheld.Vibrate();
                deathHandler.HandleDeathCondition(false);
                print("you Lost");
                Destroy(oldText);
            }
        }
    }
}
