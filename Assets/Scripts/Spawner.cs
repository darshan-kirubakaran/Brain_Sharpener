using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Color[] colors;
    public GameObject block;
    public GameObject text;
    public Transform blocksParent;
    int randomSpawner;
    float randomWaitTime;
    public float minTimeBetweenSpawns = 3;
    public float maxTimeBetweenSpawns = 5;
    public AudioClip righClickSound;

    public bool textMode = false;

    Queue<GameObject> BlockQueue = new Queue<GameObject>();

    GameSession gameSession;
    DeathHandler deathHandler;
    Queue queue;
    CountDownTimer countDownTimer;
    AudioSource audioSource;

    public bool canSpawn = true;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FallingColorsMode") == 1)
        {
            textMode = false;
        }
        else
        {
            textMode = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        deathHandler = FindObjectOfType<DeathHandler>();
        countDownTimer = FindObjectOfType<CountDownTimer>();

        audioSource = GetComponent<AudioSource>();

        if (textMode)
        {
            StartCoroutine(RandomTextFalling());
        }
        else
        {
            StartCoroutine(RandomBlockFalling());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<CountDownTimer>())
        {
            minTimeBetweenSpawns = countDownTimer.ReturnMinTimeBetweenSpawns();

            maxTimeBetweenSpawns = countDownTimer.ReturnMaxTimeBetweenSpawns();
        }
    }

    public IEnumerator RandomBlockFalling()
    {
        while(canSpawn)
        {
            randomSpawner = Random.Range((int)-((Screen.width) / Screen.dpi), (int)(Screen.width / Screen.dpi));
            randomWaitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            var newBlock = Instantiate(block, new Vector3(randomSpawner + .20f, (Screen.height / Screen.dpi), 1f), Quaternion.identity);
            newBlock.transform.parent = blocksParent;
            BlockQueue.Enqueue(newBlock);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
    
    public IEnumerator RandomTextFalling()
    {
        while(canSpawn)
        {
            randomSpawner = Random.Range((int)-((Screen.width) / Screen.dpi), (int)(Screen.width / Screen.dpi));
            randomWaitTime = Random.Range(1, 3);
            var newText = Instantiate(text, new Vector3(randomSpawner + .20f, (Screen.height / Screen.dpi), 1f), Quaternion.identity);
            newText.transform.parent = blocksParent;
            /*if(FindObjectOfType<Queue>())
            {
                queue = FindObjectOfType<Queue>();
                queue.AddToQueue(newBlock);
            }*/
            BlockQueue.Enqueue(newText);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }

    public void CheckTextIfRight(string colorName)
    {
        if(BlockQueue.Count > 0)
        {
            var oldBlock = BlockQueue.Dequeue();

            if (oldBlock.GetComponentInChildren<TextMesh>().text.ToLower() == colorName.ToLower())
            {
                // Right

                audioSource.PlayOneShot(righClickSound);
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
        if(BlockQueue.Count > 0)
        {
            var oldText = BlockQueue.Dequeue();

            if (oldText.GetComponentInChildren<TextMesh>().color == color)
            {
                // Right

                audioSource.PlayOneShot(righClickSound);
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

    public void StopSpawning()
    {
        canSpawn = false;
    }

    public void ResumeSpawning()
    {
        canSpawn = true;
    }
}
