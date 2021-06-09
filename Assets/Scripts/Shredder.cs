using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    DeathHandler deathHandler;

    private void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        deathHandler.HandleDeathCondition(false);
    }
}
