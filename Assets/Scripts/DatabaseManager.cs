using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBreference;
    public FirebaseDatabase fb;

    //User Data variables
    [Header("UserData")]
    public GameObject scoreElement;
    public Transform fallingColorsscoreboardContent;
    public Transform fallingTextscoreboardContent;
    public Transform schulteTablesHardScoreboardContent;
    public Transform schulteTablesEasyScoreboardContent;
    public Transform memoryMatrixScoreboardContent;
    public Transform mathAddAttackScoreboardContent;
    public Transform mathSubAttackScoreboardContent;
    public Transform mathMulAttackScoreboardContent;
    public Transform mathDivAttackScoreboardContent;
    public TextMeshProUGUI warningText;

    public int Lastscore = 0;

    InistializeFirebase inistializeFirebase;
    SceneLoader sceneLoader;

    void Awake()
    {
        inistializeFirebase = FindObjectOfType<InistializeFirebase>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
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


        });
    }

    private void InitializeFirebase()
    {
        //Set the authentication instance object        
        DBreference = inistializeFirebase.DBreference;

    }

    public void SaveDataButton(int Highscore, string gameName)
    {
        StartCoroutine(UpdateHighscore(Highscore, gameName));
    }

    public void FallingColorsScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadFallingColorsScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void FallingTextScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadFallingTextScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void SchulteTablesHardScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadSchulteTablesHardScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void SchulteTablesEasyScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadSchulteTablesEasyScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void MemoryMatrixScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadMemoryMatrixScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void MathAddAttackScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadMathAddAttackScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }
    
    public void MathSubAttackScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadMathSubAttackScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }
    
    public void MathMulAttackScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadMathMulAttackScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }
    
    public void MathDivAttackScoreboardButton()
    {
        if (inistializeFirebase.isLogedIn)
        {
            warningText.enabled = false;
            StartCoroutine(LoadMathDivAttackScoreboardData());
        }
        else
        {
            warningText.enabled = true;
        }
    }

    public void LoadUserData(TextMeshProUGUI score, string gameName)
    {
        StartCoroutine(LoadUsersData(score, gameName));
    }

    public int ReturnUserScore(string gameName)
    {
        StartCoroutine(ReturnUsersScore(gameName));
        return Lastscore;
    }

    public void UpdateUsername(string username)
    {
        StartCoroutine(UpdateUsernameDatabase(username));
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(inistializeFirebase.User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateHighscore(int _highscore, string gameName)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(inistializeFirebase.User.UserId).Child(gameName).SetValueAsync(_highscore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }

    private IEnumerator LoadUsersData(TextMeshProUGUI scoreText, string gameName)
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(inistializeFirebase.User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            scoreText.GetComponent<TextMeshProUGUI>().text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            print("(DatabaseManger) GameNem = " + gameName);
            print("(DatabaseManager) Value = " + snapshot.Child(gameName).Value);
            scoreText.GetComponent<TextMeshProUGUI>().text = (snapshot.Child(gameName).Value).ToString();
        }
    }

    public IEnumerator ReturnUsersScore(string gameName)
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(inistializeFirebase.User.UserId).Child(gameName).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            Lastscore = 0;
            print("No value set so Last Score = " + Lastscore);
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            Lastscore = int.Parse(snapshot.Value.ToString());
            print("GameName = " + gameName + " Last score = " + Lastscore);
        }
    }

    private IEnumerator LoadFallingTextScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("FallingText").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in fallingTextscoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("FallingText").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("FallingText").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, fallingTextscoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadFallingColorsScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("FallingColors").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in fallingColorsscoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {

                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("FallingColors").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("FallingColors").Value.ToString());
                }


                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, fallingColorsscoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadSchulteTablesHardScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("SchulteTables-Hard").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in schulteTablesHardScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("SchulteTables-Hard").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("SchulteTables-Hard").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, schulteTablesHardScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadSchulteTablesEasyScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("SchulteTables-Easy").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in schulteTablesEasyScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                if (childSnapshot.Child("SchulteTables-Easy").Value != null)
                {
                    int xp = int.Parse(childSnapshot.Child("SchulteTables-Easy").Value.ToString());

                    float minutes = Mathf.FloorToInt(xp / 60);
                    float seconds = Mathf.FloorToInt(xp % 60);

                    //Instantiate new scoreboard elements
                    GameObject scoreboardElement = Instantiate(scoreElement, schulteTablesEasyScoreboardContent);
                    scoreboardElement.GetComponent<ScoreElement>().NewScoreElementForTime(username, minutes + ":" + seconds);

                    if (username == inistializeFirebase.username)
                    {
                        usernameExists = true;
                        scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                    }
                }
            }

            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadMemoryMatrixScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("MemoryMatrix").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in memoryMatrixScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("MemoryMatrix").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("MemoryMatrix").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            // Instantiate users score
            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, memoryMatrixScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadMathAddAttackScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("MathAddAttack").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in mathAddAttackScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("MathAddAttack").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("MathAddAttack").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, mathAddAttackScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            // Instantiate users score
            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, mathAddAttackScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadMathSubAttackScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("MathSubAttack").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in mathSubAttackScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("MathSubAttack").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("MathSubAttack").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, mathSubAttackScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            // Instantiate users score
            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, mathSubAttackScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadMathMulAttackScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("MathMulAttack").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in mathMulAttackScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("MathMulAttack").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("MathMulAttack").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, mathMulAttackScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            // Instantiate users score
            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, mathMulAttackScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }
    
    private IEnumerator LoadMathDivAttackScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("MathDivAttack").LimitToLast(5).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in mathDivAttackScoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            bool usernameExists = false;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int xp = 0;
                if (childSnapshot.Child("MathDivAttack").Value != null)
                {
                    xp = int.Parse(childSnapshot.Child("MathDivAttack").Value.ToString());
                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, mathDivAttackScoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, xp);

                if (username == inistializeFirebase.username)
                {
                    usernameExists = true;
                    scoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
                }
            }

            // Instantiate users score
            if (!usernameExists)
            {
                GameObject myUserScoreboardElement = Instantiate(scoreElement, mathDivAttackScoreboardContent);
                myUserScoreboardElement.GetComponent<ScoreElement>().NewScoreElement(inistializeFirebase.username, (int)inistializeFirebase.gameScores[4]);
                myUserScoreboardElement.GetComponent<Image>().color = new Color32(10, 32, 46, 255);
            }

            //Go to scoareboard screen
        }
    }
}
