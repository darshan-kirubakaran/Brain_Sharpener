using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    DeathHandler deathHandler;
    MA_LevelManager ma_LevelManager;
    Spawner spawner;

    private void Awake()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
        ma_LevelManager = FindObjectOfType<MA_LevelManager>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (spawner.mathAttack)
        {
            PlayerPrefs.SetInt(ma_LevelManager.symbol + "Level", ma_LevelManager.level);
        }

        deathHandler.HandleDeathCondition(false);
    }
}
