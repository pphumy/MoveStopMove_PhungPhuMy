using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICSkinShop : UICanvas
{
    public void OnEnable()
    {
        UIManager.Ins.player.ChooseSkinAnim();    
    }

    public void OnDisable()
    {
        UIManager.Ins.player.ExitSkinAnim();        
    }

    public void ExitSkinShop()
    {
        UIManager.Ins.player.playerSkin.OnInit();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        CinemachineManager.Ins.SwitchToStartGameCam();
        Close();
    }
}
