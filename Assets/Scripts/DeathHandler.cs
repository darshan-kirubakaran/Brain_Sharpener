using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathHandler : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject warningText;

    public string gameName;

    int lastScore;

    public int demoValue = 1;

    public bool hasHandledDeathCondition = false;

    GameSession gameSession;
    Spawner spawner;
    CubeSpawner cubeSpawner;
    DatabaseManager databaseManager;
    InistializeFirebase inistializeFirebase;
    Demo demo;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        spawner = FindObjectOfType<Spawner>();
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        databaseManager = FindObjectOfType<DatabaseManager>();
        inistializeFirebase = FindObjectOfType<InistializeFirebase>();
        demo = FindObjectOfType<Demo>();

        if(FindObjectOfType<Spawner>())
        {
            if (spawner.textMode == true)
            {
                gameName = "FallingText";
                demoValue = 2;
            }
            else
            {
                gameName = "FallingColors";
                demoValue = 1;
            }
        }

        if (FindObjectOfType<CubeSpawner>() && FindObjectOfType<CubeSpawner>().MemoryMatrix == false)
        {

            if (cubeSpawner.continuousMode == true)
            {
                gameName = "SchulteTables-Hard";
                demoValue = 2;
            }
            else
            {
                gameName = "SchulteTables-Easy";
                demoValue = 1;
            }
        }

        print("Death handler setting gamename as = " + gameName);

        demo.DisableDemoCanvas(1);
        demo.DisableDemoCanvas(2);

        if (!PlayerPrefs.HasKey(gameName + "_demo"))
        {
            Time.timeScale = 1;
            hasHandledDeathCondition = true;

            demo.EnableDemoCanvas(demoValue);
            PlayerPrefs.SetInt(gameName + "_demo", 1);
        }
        else
        {
            demo.DisableDemoCanvas(demoValue);
        }


        if (inistializeFirebase.isLogedIn == true)
        {
            lastScore = databaseManager.ReturnUserScore(gameName);
        }

        Panel.SetActive(false);
    }

    public void HandleDeathCondition(bool smallerFirst)
    {
        if(hasHandledDeathCondition == false)
        {
            print("GameName is " + gameName);
            if (inistializeFirebase.isLogedIn == true)
            {
                warningText.SetActive(false);
                int dbLastScore = databaseManager.ReturnUserScore(gameName);
                if (!smallerFirst)
                {
                    if (dbLastScore < gameSession.ReturnScore())
                    {
                        print("values = " + dbLastScore + "   " + gameSession.ReturnScore());
                        databaseManager.SaveDataButton(gameSession.ReturnScore(), gameName);
                    }
                }
                else
                {
                    if (dbLastScore > gameSession.ReturnScore() || dbLastScore == 0)
                    {

                        print("values = " + dbLastScore + "   " + gameSession.ReturnScore());
                        databaseManager.SaveDataButton(gameSession.ReturnScore(), gameName);
                    }
                }
                print("LoadUserData");
                databaseManager.LoadUserData(highscoreText, gameName);
                print("Saved Player Data");
                SaveSystem.SavePlayer(FindObjectOfType<GameSession>(), gameName);
                print("File path = " + Application.persistentDataPath + "/" + gameName + ".fun");
                PlayerData data = SaveSystem.LoadPlayer(gameName);
                print(data.scores);
            }
            else
            {
                print("Did not login");
                warningText.SetActive(true);
            }
            Panel.SetActive(true);
            scoreText.text = gameSession.ReturnScore().ToString();
            hasHandledDeathCondition = true;
        }

    }
}
