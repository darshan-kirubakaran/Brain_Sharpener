using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberChecker : MonoBehaviour
{
    public int scorePerRound = 1;
    public List<int> numberList;
    public TextMeshProUGUI currentNumberText;
    public AudioClip righClickSound;

    CubeSpawner cubeSpawner;
    MM_LevelManager mm_LevelManager;
    SceneLoader sceneLoader;
    GameSession gameSession;
    DeathHandler deathHandler;
    Timer timer;
    Stopwatch stopwatch;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        mm_LevelManager = FindObjectOfType<MM_LevelManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        deathHandler = FindObjectOfType<DeathHandler>();
        timer = FindObjectOfType<Timer>();
        stopwatch = FindObjectOfType<Stopwatch>();

        audioSource = GetComponent<AudioSource>();

        CreateList();

        if(cubeSpawner.MemoryMatrix == false)
        {
            currentNumberText.text = numberList[0].ToString();
        }
    }

    private void CreateList()
    {
        int Count = 1;

        while (Count <= cubeSpawner.numberOfBlock)
        {
            numberList.Add(Count);
            Count++;
        }
    }
    
    public void CheckIfRight(int numberToCheck, GameObject numBlock)
    {
        if(numberToCheck == numberList[0])
        {
            numberList.RemoveAt(0);

            audioSource.PlayOneShot(righClickSound);

            if (cubeSpawner.continuousMode)
            {
                numBlock.name = (cubeSpawner.highestNumber + 1).ToString();
                cubeSpawner.highestNumber += 1;
                numBlock.GetComponentInChildren<TextMeshProUGUI>().text = numBlock.name;
                numberList.Add(cubeSpawner.highestNumber);
                currentNumberText.text = numberList[0].ToString();
            }
            else
            {
                if (numberList.Count == 0)
                {
                    //Game Over
                    gameSession.addToScore((int)stopwatch.ReturnTime());
                    stopwatch.StopStopwatch();
                    deathHandler.HandleDeathCondition(true);
                }
                else
                {
                    currentNumberText.text = numberList[0].ToString();
                }
            }
        }
        else
        {
            // You Lost
            Handheld.Vibrate();
        }
    }
    
    public void CheckMemoryMatrix(bool rightCube)
    {
        if(rightCube)
        {
            // got it right
            print("correct");
            audioSource.PlayOneShot(righClickSound);

            
        }
        else
        {
            // You Lost
            print("wrong");
            StartCoroutine(FailedMemoryMatrix());
        }

        int numOfClickedCubes = 0; ;

        foreach (Transform child in cubeSpawner.numCubeParent.transform)
        {
            if (child.GetComponent<MemoryMatrixColorBlock>().rightCube)
            {

                if (child.GetComponent<MemoryMatrixColorBlock>().hasBeenClicked)
                {
                    numOfClickedCubes += 1;
                }
            }
        }
        if (numOfClickedCubes == cubeSpawner.totalRightCubes)
        {
            foreach (Transform child in cubeSpawner.numCubeParent.transform)
            {
                child.GetComponent<MemoryMatrixColorBlock>().canClick = false;
            }

            // Round Over
            print("ROUND OVER");
            StartCoroutine(NewRound());

        }
    }

    private IEnumerator FailedMemoryMatrix()
    {
        mm_LevelManager.resultImage.sprite = mm_LevelManager.wrongMark;
        mm_LevelManager.resultImage.enabled = true;

        Handheld.Vibrate();

        foreach (Transform child in cubeSpawner.numCubeParent)
        {
            child.GetComponent<MemoryMatrixColorBlock>().ChangeColorOfCubeToDefault();
        }

        if (mm_LevelManager.currentLevel != 1)
        {
            mm_LevelManager.currentLevel = mm_LevelManager.currentLevel - 1;
        }
        print("cuurentLevel = " + mm_LevelManager.currentLevel);
        mm_LevelManager.currentRound = 1;
        PlayerPrefs.SetInt("MM_CurrentLevel", mm_LevelManager.currentLevel);
        mm_LevelManager.levelText.text = "Level " + mm_LevelManager.currentLevel;

        yield return new WaitForSeconds(1);
        mm_LevelManager.resultImage.enabled = false;

        deathHandler.HandleDeathCondition(false);
    }

    private IEnumerator NewRound()
    {
        mm_LevelManager.resultImage.sprite = mm_LevelManager.TickMark;
        mm_LevelManager.resultImage.enabled = true;
        yield return new WaitForSeconds(1);
        mm_LevelManager.resultImage.enabled = false;
        mm_LevelManager.NextRound();
    }
}
