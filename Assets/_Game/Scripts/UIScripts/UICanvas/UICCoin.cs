using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICCoin : UICanvas
{
    public Text coinsText;

    private void OnEnable()
    {
        coinsText.text = PlayerDataController.Ins.LoadFromJson().coins.ToString();
    }
}
