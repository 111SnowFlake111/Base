using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : BaseBox
{
    public static Setting instance;

    public static Setting Setup(bool smallReward = false, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<Setting>(PathPrefabs.GAME_SETTING));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button soundBtn;
    public Button vibrationBtn;
    public Button closeWindow;

    public Image soundDisabled;
    public Image vibrationDisabled;

    private void Init()
    {
        soundBtn.onClick.AddListener(delegate { SoundChange(); });
        vibrationBtn.onClick.AddListener(delegate { VibrationChange(); });
        closeWindow.onClick.AddListener(Close);
    }

    private void InitState()
    {
        
    }

    private void SoundChange()
    {
        if (AudioListener.volume == 1)
        {
            soundDisabled.gameObject.SetActive(true);
            AudioListener.volume = 0;
        }
        else
        {
            soundDisabled.gameObject.SetActive(false);
            AudioListener.volume = 1;
        }

        
        Debug.LogError("Audio changed");
    }

    private void VibrationChange()
    {
        Handheld.Vibrate();
        if (vibrationDisabled.gameObject.activeSelf)
        {
            vibrationDisabled.gameObject.SetActive(false);
        }
        else
        {
            vibrationDisabled.gameObject.SetActive(true);
        }
        Debug.LogError("Vibration changed");
    }
}