using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public Image SettingImage;
    public Image BestScoreImage;
    public Image VolumeImage;
    public Image LoginImage;
    public Image RegisterImage;
    public Image ProfileImage;
    public Image MenuImage;

    public Text scoreText;

    public AudioSource myAudio;
    public Slider volumeSlider;

    public string bestScore;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        bestScore = PlayerPrefs.GetString("bestscore");
        scoreText.text = bestScore;
        myAudio.volume=volumeSlider.value;
    }

    public void Setting()
    {
        SettingImage.gameObject.SetActive(true);
    }
    public void Exit()
    {
        SettingImage.gameObject.SetActive(false);
    }

    public void BestScore()
    {
        BestScoreImage.gameObject.SetActive(true);
        StartCoroutine(Main.Instance.Web.BestScore());
    }
    public void ExitBestScore()
    {
        BestScoreImage.gameObject.SetActive(false);
    }

    public void Volume()
    {
        VolumeImage.gameObject.SetActive(true);
    }
    public void ExitVolume()
    {
        VolumeImage.gameObject.SetActive(false);
    }

    public void Login()
    {
        LoginImage.gameObject.SetActive(true);
    }
    public void LoginExit()
    {
        LoginImage.gameObject.SetActive(false);
    }

    public void Register()
    {
        RegisterImage.gameObject.SetActive(true);
    }
    public void RegisterExit()
    {
        RegisterImage.gameObject.SetActive(false);
    }

    public void Profile()
    {
        ProfileImage.gameObject.SetActive(true);
    }
    public void ProfileExit()
    {
        ProfileImage.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
}
