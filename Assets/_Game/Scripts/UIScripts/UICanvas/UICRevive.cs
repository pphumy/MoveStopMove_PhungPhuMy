using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICRevive : UICanvas
{
    public int timeCountDown;
    public Text reviveText;

    private void OnEnable()
    {
        StartCoroutine(CountDownCanvas());
    }

    IEnumerator CountDownCanvas()
    {
        while (timeCountDown != 0)
        {
            reviveText.text = timeCountDown.ToString();
            yield return new WaitForSeconds(1f);
            timeCountDown -= 1;
        }
        OnCloseRevive();
    }

    public void OnCloseRevive()
    {
        UIManager.Ins.player.Lose();
        UIManager.Ins.OpenUI(UIID.UICFail);
        Close();
    }

    public void Revive()
    {
        UIManager.Ins.player.Revive();
        UIManager.Ins.OpenUI(UIID.UICGameplay);
        Close();
    }
}
