using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
    //public Player player;
    public Text playerText;
    public Text placeHolderText;
    public Image progressFill;
    public List<Sprite> levelSprites;
    public Image levelIcon;
    public Text zoneText;
    public Text bestRank;

    private void OnEnable()
    {
        placeHolderText.text = PlayerDataController.Ins.LoadFromJson().name;
        playerText.text = PlayerDataController.Ins.LoadFromJson().name;
        progressFill.rectTransform.localScale = new Vector3(PlayerDataController.Ins.LoadFromJson().progress, 1, 1);
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        int level = data.level;
        if (level >= levelSprites.Count) level = 0;
        levelIcon.sprite = levelSprites[level];
        zoneText.text = "Zone: " + (level + 1).ToString();
        bestRank.text = "Best: $" + data.bestRank.ToString();

    }

    public void PlayGame()
    {
        UIManager.Ins.OpenUI(UIID.UICGameplay);
        LevelManager.Ins.SetGameState(Constant.GameState.PLAY);
        CinemachineManager.Ins.SwitchToPlayCam();
        UIManager.Ins.GetUI(UIID.UICCoin).Close();
        Close();
    }

    public void OpenWeaponShop()
    {
        UIManager.Ins.OpenUI(UIID.UICWeaponShop);
        Close();
    }

    public void OpenSkinShop()
    {
        UIManager.Ins.OpenUI(UIID.UICSkinShop);
        CinemachineManager.Ins.SwitchToSkinShopCam();
        Close();
    }

    public void ChangeName()
    {
        string newName = playerText.text;
        if (newName.Length > 0)
        {
            PlayerData data = PlayerDataController.Ins.LoadFromJson();
            data.name = newName;
            PlayerDataController.Ins.SaveToJson(data);
            UIManager.Ins.player.InitName();
        }
    }
}
