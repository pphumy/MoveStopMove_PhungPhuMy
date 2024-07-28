using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICFail : UICanvas
{
    public Text rankingText;
    public Text killerName;
    public Text coinText;

    public Image progressFill;
    public Image bestProgressFill;

    
    private void OnEnable()
    {
        rankingText.text = "$" + (LevelManager.Ins.GetRemainNumOfBots() + 1).ToString();
        killerName.text = LevelManager.Ins.GetFinalKiller().GetName().ToString();
        float newProgress = (float)LevelManager.Ins.GetNumOfBotsDie() / (float)LevelManager.Ins.GetNumOfTotalBots();
        progressFill.rectTransform.localScale = new Vector3(newProgress, 1, 1);
        bestProgressFill.rectTransform.localScale = new Vector3(PlayerDataController.Ins.LoadFromJson().progress, 1, 1);
        coinText.text = UIManager.Ins.player.GetScore().ToString();
    }

    public void PlayAgain()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        LevelManager.Ins.StartGame(playerData.level);
    }

    public void IncreaseScore()
    {
        UIManager.Ins.player.MultipleScore(3);
        PlayAgain();
    }
}
