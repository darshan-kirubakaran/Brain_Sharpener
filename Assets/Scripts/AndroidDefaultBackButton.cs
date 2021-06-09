using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidDefaultBackButton : MonoBehaviour
{
    private void Start()
    {
        SetUpSingelton();
    }

    private void SetUpSingelton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                var currentScene = SceneManager.GetActiveScene().name;

                if (currentScene == "Falling Colors" || currentScene == "Number Box") // Add new games to this if statement
                {
                    SceneManager.LoadScene("Games Menu");
                }
                else if(currentScene == "Main Menu")
                {
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadScene("Main Menu");
                }
            }
        }
    }
}
