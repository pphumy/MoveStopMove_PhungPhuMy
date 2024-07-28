using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateButton : MonoBehaviour
{
    public GameObject vibrateButtonOn;
    public GameObject vibrateButtonOff;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(Constant.VIBRATE_ON, 1) == 1)
        {
            vibrateButtonOn.SetActive(true);
            vibrateButtonOff.SetActive(false);
            SoundManager.Ins.TurnVibrateOn();
        }
        else
        {
            vibrateButtonOn.SetActive(false);
            vibrateButtonOff.SetActive(true);
            SoundManager.Ins.TurnVibrateOff();
        }
    }

    public void SetOnOffVibrate()
    {
        var soundValue = PlayerPrefs.GetInt(Constant.VIBRATE_ON);
        if (soundValue == 0)
        {
            vibrateButtonOn.SetActive(true);
            vibrateButtonOff.SetActive(false);
            SoundManager.Ins.TurnVibrateOn();
        }
        else if (soundValue == 1)
        {
            vibrateButtonOn.SetActive(false);
            vibrateButtonOff.SetActive(true);
            SoundManager.Ins.TurnVibrateOff();
        }
    }
}
