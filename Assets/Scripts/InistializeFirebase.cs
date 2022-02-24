using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InistializeFirebase : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public DatabaseReference DBreference;
    public FirebaseUser User;
    public string username;
    public enum GAMENAMES { HITCOLOR, HITTEXT, TAPIN60, TAP25, MEMORYMASTER};

    public string[] gameNames = { "FallingText", "FallingColors", "SchulteTables-Hard", "SchulteTables-Easy", "MemoryMatrix", "MathAddAttack", "MathSubAttack", "MathMulAttack", "MathDivAttack" };
    public float[] gameScores = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public bool finshedInistializingFirebase = false;
    public bool isLogedIn = false;

    public void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

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

    private void Start()
    {
        StartCoroutine(Waiter());
    }

    public void InitializeFirebase()
    {
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        Debug.Log("Finished setting up Firebase Auth");
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Finished setting up DBrefrence " + DBreference);
        if(DBreference != null)
        {
            print("DBreference != null");
            finshedInistializingFirebase = true;
            print("FinishedSettingupFirebase");
        }
    }

    public IEnumerator Waiter()
    {
        yield return new WaitUntil(predicate: () => finshedInistializingFirebase == true);

        if (finshedInistializingFirebase)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadAllUserdataFromFirebase()
    {
        StartCoroutine(LoadAllUserdataFromFirebaseImpl());
    }

    public IEnumerator LoadAllUserdataFromFirebaseImpl()
    {
        print("UserId = " + User.UserId);

        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();
       
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet

        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            int count = 0;

            foreach (string gamename in gameNames)
            {
                if(snapshot.Child(gamename).Value != null)
                {
                    gameScores[count] = int.Parse(snapshot.Child(gamename).Value.ToString());
                    print(gamename + " " + gameScores[count]);
                    count++;
                }
            }

            username = snapshot.Child("username").Value.ToString();

            print("username = " + username);
        }
    }

    private void LoadSplashScreen()
    {
        SceneManager.LoadScene(1);
    }
}