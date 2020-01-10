﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioClip buttonSound;
    private Scene mainLevel;
    private Scene mainMenu;
    public GameObject startMenu, optionMenu, igMenu;
    public void Awake() 
    {
        mainLevel = SceneManager.GetSceneByName("Main Level");
        mainMenu = SceneManager.GetSceneByName("Main Menu");
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void StartButton()
    {
        AudioManager.PlaySound(buttonSound, AudioManager.AudioGroups.UISFX);
        LoadNewLevel(mainLevel);
    }
    public void Options()
    {
        AudioManager.PlaySound(buttonSound, AudioManager.AudioGroups.UISFX);
        startMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void Back()
    {
        AudioManager.PlaySound(buttonSound, AudioManager.AudioGroups.UISFX);
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void LoadNewLevel(Scene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionIG()
    {
        AudioManager.PlaySound(buttonSound, AudioManager.AudioGroups.UISFX);
        igMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        AudioManager.PlaySound(buttonSound, AudioManager.AudioGroups.UISFX);
        LoadNewLevel(mainMenu);   
    }
}
