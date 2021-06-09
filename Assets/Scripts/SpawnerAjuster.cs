using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAjuster : MonoBehaviour
{
    public int spawnerNumber;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(((Screen.width / 5) / 64) * spawnerNumber, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
