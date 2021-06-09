using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.25f;

    [SerializeField] Slider difficultySlider;
    [SerializeField] int defaultDifficulty = 0;

    GameObject optionsCanvas;

    MusicPlayer musicPlayer;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        optionsCanvas = GameObject.Find("Options Canvas");

        if (PlayerPrefs.HasKey("master volume") == false)
        {
            PlayerPrefsController.SetMasterVolume(defaultVolume);
        }

        if (musicPlayer && volumeSlider != null)
        {

            volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        }
        //difficultySlider.value = PlayerPrefsController.GetDifficulty();

        if (musicPlayer && volumeSlider != null)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found... did you start from splash screen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (musicPlayer && volumeSlider != null)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            //Debug.LogWarning("No music player found... did you start from splash screen");
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        //PlayerPrefsController.SetDifficulty((int)difficultySlider.value);
        sceneLoader.DisableOptionsCanvas();
    }

    public void SetDefault()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
    }
}
