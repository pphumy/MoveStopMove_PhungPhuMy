using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;

    public List<AudioClip> throwSound;
    public List<AudioClip> dieSound;
    public AudioClip victorySound;
    public AudioClip loseSound;
    public AudioClip sizeUpSound;

    private bool soundOn;
    private bool vibrateOn;

    private void OnEnable()
    {
        soundOn = (PlayerPrefs.GetInt(Constant.SOUND_ON, 1) == 1);
        vibrateOn = (PlayerPrefs.GetInt(Constant.VIBRATE_ON, 1) == 1);
    }

    public void TurnOnSound()
    {
        soundOn = true;
        PlayerPrefs.SetInt(Constant.SOUND_ON, 1);
    }

    public void TurnOffSound()
    {
        soundOn = false;
        PlayerPrefs.SetInt(Constant.SOUND_ON, 0);
    }

    public void TurnVibrateOn()
    {
        vibrateOn = true;
        PlayerPrefs.SetInt(Constant.VIBRATE_ON, 1);
    }

    public void TurnVibrateOff()
    {
        vibrateOn = false;
        PlayerPrefs.SetInt(Constant.VIBRATE_ON, 0);
    }

    public void PlayThrowSound()
    {
        if (soundOn)  audioSource.PlayOneShot(throwSound[Random.Range(0, throwSound.Count)]);
    }

    public void PlayDieSound()
    {
        if (soundOn)  audioSource.PlayOneShot(dieSound[Random.Range(0, dieSound.Count)]);
        if (vibrateOn)  Handheld.Vibrate();
    }

    public void PlayLoseSound()
    {
        if (soundOn) audioSource.PlayOneShot(loseSound);
        if (vibrateOn) Handheld.Vibrate();
    }

    public void PlayVictorySound()
    {
        if (soundOn) audioSource.PlayOneShot(victorySound);
        if (vibrateOn) Handheld.Vibrate();
    }

    public void PlaySizeUpSound()
    {
        if (soundOn) audioSource.PlayOneShot(sizeUpSound);
        if (vibrateOn) Handheld.Vibrate();
    }

    public bool IsSoundOn()
    {
        return soundOn;
    }

    public bool IsVibrateOn()
    {
        return vibrateOn;
    }
}
