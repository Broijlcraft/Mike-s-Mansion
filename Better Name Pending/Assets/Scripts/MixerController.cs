﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider, masterSlider;
    public string musicSliderName, sfxSliderName, masterSliderName;
    public GameObject canvas;

    public static MixerController instance;
            
    public void Awake()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas) {
            Transform[] t = GetComponentsInChildren<Transform>();
            if(Array.Exists(t, element => element.transform.tag == masterSliderName)) {
                //for (int i = 0; i < transform)
                //masterSlider = 
            }
            print(canvas);
        }

        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start() 
    {
        float master = PlayerPrefs.GetFloat("masterVolume", 0f);
        SetMasterVolume(master);
        float music = PlayerPrefs.GetFloat("musicVolume", 0f);
        SetMusicVolume(music);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 0f);
        SetSFXVolume(sfx);
    }
    public void SetMasterVolume(float sliderValue)
    {
        sliderValue = masterSlider.value;
        audioMixer.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float sliderValue)
    {
        sliderValue = musicSlider.value;
        audioMixer.SetFloat("musicVolume", sliderValue);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float sliderValue)
    {
        sliderValue = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.Save();
    }
}
