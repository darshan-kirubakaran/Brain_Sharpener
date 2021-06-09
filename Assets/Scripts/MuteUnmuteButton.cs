using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteUnmuteButton : MonoBehaviour
{
    public Sprite muteButton;
    public Sprite unmuteButton;

    public float previousVolume;

    public bool isMute;

    MusicPlayer musicPlayer;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        musicPlayer = FindObjectOfType<MusicPlayer>();

        image.sprite = unmuteButton;

        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteButton()
    {
        if(isMute == true)
        {
            isMute = false;
            image.sprite = unmuteButton;
            musicPlayer.Unmute();
        }
        else if(isMute == false)
        {
            isMute = true;
            image.sprite = muteButton;
            musicPlayer.Mute();

        }
    }


}
