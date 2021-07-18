using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Color[] colors;
    public GameObject[] fallingObjects;
    public GameObject block;
    public GameObject text;
    public Transform blocksParent;
    int randomSpawner;
    float randomWaitTime;
    public float minTimeBetweenSpawns = 3;
    public float maxTimeBetweenSpawns = 5;
    public AudioClip righClickSound;

    public bool textMode = false;
    public bool mathAttack = false;

    public Queue<GameObject> BlockQueue = new Queue<GameObject>();

    GameObject fallingObject;

    GameSession gameSession;
    DeathHandler deathHandler;
    Queue queue;
    CountDownTimer countDownTimer;
    MA_LevelManager ma_levelmanager;
    MA_ChoiceManager ma_ChoiceManger;
    public AudioSource audioSource;

    public bool canSpawn = true;

    public int totalSpawns = 0;

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
        ma_levelmanager = FindObjectOfType<MA_LevelManager>();
        ma_ChoiceManger = FindObjectOfType<MA_ChoiceManager>();

        audioSource = GetComponent<AudioSource>();

        if(mathAttack)
        {
            fallingObject = fallingObjects[Random.Range(0, fallingObjects.Length)];
            StartCoroutine(RandomMAFallingBlocks());
        }
        else if (textMode)
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
    
    public IEnumerator RandomMAFallingBlocks()
    {
        while(canSpawn)
        {
            randomSpawner = Random.Range((int)-((Screen.width) / Screen.dpi), (int)(Screen.width / Screen.dpi));
            randomWaitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            var newBlock = Instantiate(fallingObject, new Vector3(randomSpawner + .20f, (Screen.height / Screen.dpi), 1f), Quaternion.identity);
            BlockQueue.Enqueue(newBlock);
            newBlock.transform.parent = blocksParent;

            if (BlockQueue.Count == 1)
            {
                GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>().text = "B = " + BlockQueue.Count.ToString();
                ma_ChoiceManger.ChoiceValueChanger();
            }

            totalSpawns++;

            if(totalSpawns > ma_levelmanager.lcount)
            {
                print("Next Level");

                //ma_levelmanager.currentLevel++;
                //PlayerPrefs.SetInt("MA_CurrentLevel", ma_levelmanager.currentLevel);

                ma_levelmanager.RunLevels(1);

                totalSpawns = 0;
            }

            yield return new WaitForSeconds(randomWaitTime);
            GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>().text = "New Spawn";
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

    public void StopSpawning()
    {
        canSpawn = false;
        GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>().text = "Paused";
    }

    public void ResumeSpawning()
    {
        canSpawn = true;
        GameObject.Find("Debug Text").GetComponent<TextMeshProUGUI>().text = "Resumed";
    }
}
