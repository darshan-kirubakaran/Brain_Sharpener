using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;

    GameObject mainCanvas;
    GameObject optionsCanvas;
    GameObject pauseMenuCanvas;

    InistializeFirebase inistializeFirebase;
    DatabaseManager databaseManager;

    void Awake()
    {
        inistializeFirebase = FindObjectOfType<InistializeFirebase>();
        databaseManager = FindObjectOfType<DatabaseManager>();

        //Check that all of the necessary dependencies for Firebase are present on the system
        /*FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });*/

        if(FindObjectOfType<InistializeFirebase>())
        {
            InitializeFirebase();
        }
        
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = inistializeFirebase.auth;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //StartCoroutine(SplashScreen());
            SplashScreen();
        }*/

        if (SceneManager.GetActiveScene().name == "Leaderboard")
        {
            databaseManager.FallingColorsScoreboardButton();
            databaseManager.FallingTextScoreboardButton();
            databaseManager.SchulteTablesHardScoreboardButton();
            databaseManager.SchulteTablesEasyScoreboardButton();
            databaseManager.MemoryMatrixScoreboardButton();
            databaseManager.MathAddAttackScoreboardButton();
            databaseManager.MathSubAttackScoreboardButton();
            databaseManager.MathMulAttackScoreboardButton();
            databaseManager.MathDivAttackScoreboardButton();
        }

        optionsCanvas = GameObject.Find("Options Canvas");
        mainCanvas = GameObject.Find("Main Canvas");

        if(GameObject.Find("PauseMenu Canvas"))
        {
            pauseMenuCanvas = GameObject.Find("PauseMenu Canvas");
        }

        if (mainCanvas = GameObject.Find("Main Canvas"))
        {
            mainCanvas.SetActive(true);
        }

        if (optionsCanvas = GameObject.Find("Options Canvas"))
        {
            optionsCanvas.SetActive(false);
        }
    }

    public void GamesMenu()
    {
        SceneManager.LoadScene("Games Menu");
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Leaderboard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Leaderboard");
    }
    
    public void FallingColors()
    {
        SceneManager.LoadScene("Falling Colors");
        PlayerPrefs.SetInt("FallingColorsMode", 1);
    }
    
    public void FallingText ()
    {
        SceneManager.LoadScene("Falling Colors");
        PlayerPrefs.SetInt("FallingColorsMode", 2);
    }
    
    public void NumberBoxContinuous ()
    {
        print("(sceneloader)Schulte tables Continuous setting 1");
        SceneManager.LoadScene("Number Box");
        PlayerPrefs.SetInt("NumberBoxMode", 1);
    }
    
    public void NumberBoxFixed ()
    {
        print("(sceneloader)Schulte tables Fixed setting 2");
        SceneManager.LoadScene("Number Box");
        PlayerPrefs.SetInt("NumberBoxMode", 2);
    }

    public void MemoryMatrix()
    {
        SceneManager.LoadScene("Memory Matrix");
    }
    
    public void AddAttack()
    {
        PlayerPrefs.SetString("MathMode", "ADD");
        SceneManager.LoadScene("Math Attack");
    }

    public void SubAttack()
    {
        PlayerPrefs.SetString("MathMode", "SUB");
        SceneManager.LoadScene("Math Attack");
    }

    public void MulAttack()
    {
        PlayerPrefs.SetString("MathMode", "MUL");
        SceneManager.LoadScene("Math Attack");
    }

    public void DivAttack()
    {
        PlayerPrefs.SetString("MathMode", "DIV");
        SceneManager.LoadScene("Math Attack");
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
    }
    
    public void DisableOptionsCanvas()
    {
        optionsCanvas.SetActive(false);
    }

    public void LoadCurrentSceneAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SignOutButton()
    {
        inistializeFirebase.isLogedIn = false;
        auth.SignOut();
        PlayerPrefs.SetString("AutoLogin", "false");
        SceneManager.LoadScene(1);
    }
    
    public void LoginButton()
    {
        inistializeFirebase.isLogedIn = false;
        SceneManager.LoadScene(1);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
