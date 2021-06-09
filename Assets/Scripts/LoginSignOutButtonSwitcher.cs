using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSignOutButtonSwitcher : MonoBehaviour
{
    public GameObject signOutButton;
    public GameObject loginButton;

    InistializeFirebase inistializeFirebase;

    // Start is called before the first frame update
    void Start()
    {
        inistializeFirebase = FindObjectOfType<InistializeFirebase>();

        if(inistializeFirebase.isLogedIn == true)
        {
            signOutButton.SetActive(true);
            loginButton.SetActive(false);
        }
        else
        {
            signOutButton.SetActive(false);
            loginButton.SetActive(true);
        }
    }
}
