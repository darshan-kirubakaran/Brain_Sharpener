using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentRound = 1;

    public bool hasFinishedCountDown = false;

    public Sprite TickMark;
    public Sprite wrongMark;

    public TextMeshProUGUI levelText;

    TextMeshProUGUI countDownText;
    
    public Image resultImage;

    CubeSpawner cubeSpawner;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        gameSession = FindObjectOfType<GameSession>();

        countDownText = GameObject.Find("Count Down Text").GetComponent<TextMeshProUGUI>();
        resultImage = GameObject.Find("Result Image").GetComponent<Image>();

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        }
        
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        gameSession.addToScore((currentLevel * (currentLevel + 1) / 2) - currentLevel);
        print("Score = " + gameSession.score);

        levelText.text = "Level " + currentLevel;

        resultImage.enabled = false;

        cubeSpawner.NewSpawnMethods(currentLevel);
        //NextRound();

        StartCoroutine(StartCountdownImpl(true));
    }

    IEnumerator StartCountdownImpl(bool firstTime)
    {
        if(firstTime)
        {
            foreach (Transform child in cubeSpawner.numCubeParent)
            {
                child.GetComponent<MemoryMatrixColorBlock>().ChangeColorOfCubeToBase();
            }

            countDownText.text = "3";

            yield return new WaitForSeconds(1);

            countDownText.text = "2";

            yield return new WaitForSeconds(1);

            countDownText.text = "1";

            yield return new WaitForSeconds(1);

            foreach (Transform child in cubeSpawner.numCubeParent)
            {
                child.GetComponent<MemoryMatrixColorBlock>().ChangeColorOfCubeToDefault();
            }

            hasFinishedCountDown = true;

            countDownText.enabled = false;
        }

        countDownText.enabled = true;

        countDownText.text = "2";

        yield return new WaitForSeconds(1);

        countDownText.text = "1";

        yield return new WaitForSeconds(1);

        countDownText.enabled = false;

        foreach (Transform child in cubeSpawner.numCubeParent)
        {
            child.GetComponent<MemoryMatrixColorBlock>().ChangeColorOfCubeToBase();
        }
    }

    public void NextRound()
    {
        if (currentRound >= currentLevel)
        {
            currentLevel += 1;
            currentRound = 1;
            levelText.text = "Level " + currentLevel;
        }
        else
        {
            currentRound += 1;
        }

        PlayerPrefs.SetInt("CurrentLevel", currentLevel);

        gameSession.addToScore(1);
        print("Score = " + gameSession.score);

        cubeSpawner.ClearNumCubes();
        cubeSpawner.NewSpawnMethods(currentLevel);

        StartCoroutine(StartCountdownImpl(false));
    }
}
