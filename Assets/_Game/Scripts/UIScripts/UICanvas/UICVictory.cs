using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICVictory : UICanvas
{
    public Text unlockText;
    public Text coinText;

    private void OnEnable()
    {
        string level = (PlayerDataController.Ins.LoadFromJson().level + 2).ToString();
        unlockText.text = "Unlock Level " + level;

        coinText.text = UIManager.Ins.player.GetScore().ToString();
    }

    public void NextLevel()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
        LevelManager.Ins.StartGame(PlayerDataController.Ins.LoadFromJson().level);
    }

    public void IncreaseCoin()
    {
        UIManager.Ins.player.MultipleScore(5);
        NextLevel();
    }
}
