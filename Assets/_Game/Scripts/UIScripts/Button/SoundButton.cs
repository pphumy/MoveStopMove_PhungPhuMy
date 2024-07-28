using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public GameObject soundButtonOn;
    public GameObject soundButtonOff;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(Constant.SOUND_ON, 1) == 1)
        {
            soundButtonOn.SetActive(true);
            soundButtonOff.SetActive(false);
            SoundManager.Ins.TurnOnSound();
        }
        else
        {
            soundButtonOn.SetActive(false);
            soundButtonOff.SetActive(true);
            SoundManager.Ins.TurnOffSound();
        }
    }

    public void SetOnOffSound()
    {
        var soundValue = PlayerPrefs.GetInt(Constant.SOUND_ON);
        if (soundValue == 0)
        {
            soundButtonOn.SetActive(true);
            soundButtonOff.SetActive(false);
            SoundManager.Ins.TurnOnSound();
        }
        else if (soundValue == 1)
        {
            soundButtonOn.SetActive(false);
            soundButtonOff.SetActive(true);
            SoundManager.Ins.TurnOffSound();
        }
    }
}
