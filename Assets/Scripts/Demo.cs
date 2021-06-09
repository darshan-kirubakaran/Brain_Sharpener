using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public Canvas demoCanvas;

    DeathHandler deathHandler;
    SceneLoader sceneLoader;
    PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        
    }
    
    public void DisableDemo()
    {
        Time.timeScale = 1;
        demoCanvas.gameObject.SetActive(false);
        sceneLoader.LoadCurrentSceneAgain();

    }

    public void EnableDemoCanvas(int demoValue)
    {
        if (GameObject.Find("Demo Canvas " + demoValue))
        {
            demoCanvas = GameObject.Find("Demo Canvas " + demoValue).GetComponent<Canvas>();
            demoCanvas.enabled = true;
            print("Enabled Demo Cnavas");
        }
        else
        {
            print("Enabled Failed " + "Demo Canvas " + demoValue + "-");
        }
    }
    
    public void DisableDemoCanvas(int demoValue)
    {
        if (GameObject.Find("Demo Canvas " + demoValue))
        {
            demoCanvas = GameObject.Find("Demo Canvas " + demoValue).GetComponent<Canvas>();
            demoCanvas.enabled = false;
            print("Disabled Demo Cnavas");
        }
        else
        {
            print("Disable Failed " + "Demo Canvas " + demoValue + "-");
        }
    }
    
    public void EnableDemoCanvasWithoutDemoValue()
    {
        if (GameObject.Find("Demo Canvas " + deathHandler.demoValue))
        {
            pauseMenu.GameIsPaused = false;
            demoCanvas = GameObject.Find("Demo Canvas " + deathHandler.demoValue).GetComponent<Canvas>();
            demoCanvas.enabled = true;
            print("Enabled Demo Cnavas");
        }
        else
        {
            print("Enabled Failed " + "Demo Canvas " + deathHandler.demoValue + "-");
        }
    }
    
    public void DisableDemoCanvasWithoutDemoValue()
    {
        if (GameObject.Find("Demo Canvas " + deathHandler.demoValue))
        {
            demoCanvas = GameObject.Find("Demo Canvas " + deathHandler.demoValue).GetComponent<Canvas>();
            demoCanvas.enabled = false;
            print("Disabled Demo Cnavas");
        }
        else
        {
            print("Disable Failed " + "Demo Canvas " + deathHandler.demoValue + "-");
        }
    }

}
