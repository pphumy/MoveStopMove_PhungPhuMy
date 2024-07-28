using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : Singleton<CoinController>
{
    private int coins;
    public Text coinText;

    private void Start()
    {
        coins = PlayerDataController.Ins.LoadFromJson().coins;
    }

    public void IncreaseCoins(int increaseAmount)
    {
        coins += increaseAmount;
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        data.coins = coins;
        PlayerDataController.Ins.SaveToJson(data);
    }

    public void DecreaseCoins(int decreaseAmount)
    {
        coins -= decreaseAmount;
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        data.coins = coins;
        coinText.text = coins.ToString();
        PlayerDataController.Ins.SaveToJson(data);
    }

    public int GetCoins()
    {
        return coins;
    }
}
