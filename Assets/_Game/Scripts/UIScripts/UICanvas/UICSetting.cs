using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICSetting : UICanvas
{
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject vibrateOn;
    public GameObject vibrateOff;

    private void OnEnable()
    {
        SetSound();
        SetVibrate();
    }

    public void OnTurnOnSound()
    {
        SoundManager.Ins.TurnOnSound();
        SetSound();
    }

    public void OnTurnOffSound()
    {
        SoundManager.Ins.TurnOffSound();
        SetSound();
    }

    public void OnTurnOnVibration()
    {
        SoundManager.Ins.TurnVibrateOn();
        SetVibrate();
    }

    public void OnTurnOffVibration()
    {
        SoundManager.Ins.TurnVibrateOff();
        SetVibrate();
    }

    public void OnReturnHome()
    {
        Time.timeScale = 1;
        LevelManager.Ins.RestartGame();
        Close();
    }

    public void OnContinue()
    {
        Time.timeScale = 1;
        UIManager.Ins.OpenUI(UIID.UICGameplay);
        Close();
    }

    private void SetSound()
    {
        soundOn.SetActive(SoundManager.Ins.IsSoundOn());
        soundOff.SetActive(!SoundManager.Ins.IsSoundOn());
    }

    private void SetVibrate()
    {
        vibrateOn.SetActive(SoundManager.Ins.IsVibrateOn());
        vibrateOff.SetActive(!SoundManager.Ins.IsVibrateOn());
    }
}
