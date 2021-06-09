using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //public AudioClip song;

    AudioSource audioSource;

    void Awake()
    {
        //SetUpSingelton();
        PlayerPrefsController.SetMasterVolume(1); // TODO Remove line
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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
        //audioSource.PlayOneShot(song);
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }
        audioSource.volume = volume;
    }

    public float ReturnVolume()
    {
        if (audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }
        return audioSource.volume;
    }

    public void Mute()
    {
        if (audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }

        GameObject[] allAudioSource = GameObject.FindGameObjectsWithTag("AudioSource");

        foreach (GameObject audioSource in allAudioSource)
        {
            audioSource.GetComponent<AudioSource>().mute = true;
        }

        audioSource.mute = true;
    }
    
    public void Unmute()
    {
        if (audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }

        GameObject[] allAudioSource = GameObject.FindGameObjectsWithTag("AudioSource");

        foreach (GameObject audioSource in allAudioSource)
        {
            audioSource.GetComponent<AudioSource>().mute = false;
        }

        audioSource.mute = false;
    }
}
