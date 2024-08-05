using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public GameObject musicOnImg;
    public GameObject musicOffImg;
    public bool playMusic;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayMusic") == 0)
        {
            ActiveMusic();
        }
        else
        {
            DeactiveMusic();
        }
    }

    public void ActiveMusic()
    {
        playMusic = true;
        musicOffImg.SetActive(false);
        musicOnImg.SetActive(true);
        PlayerPrefs.SetInt("PlayMusic", 0);
        backgroundMusic.Play();
    }

    public void DeactiveMusic()
    {
        playMusic = false;
        musicOnImg.SetActive(false);
        musicOffImg.SetActive(true);
        PlayerPrefs.SetInt("PlayMusic", 1);
        backgroundMusic.Stop();
    }

    //Se está ativa ela é desativada, ou o contrário
    public void SetMusic()
    {
        if (playMusic)
        {
            DeactiveMusic();
        }
        else
        {
            ActiveMusic();
        }
    }
}
