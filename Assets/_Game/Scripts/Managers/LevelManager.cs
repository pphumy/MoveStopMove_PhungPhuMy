using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    // Characters
    public Player player;
    private Character finalKiller;

    // Game Plane
    public Transform planeHolder;
    private GameObject gamePlane;

    // Level datas
    public List<LevelData> levelDatas;

    // Variables in one level
    private int level;
    private Constant.GameState gameState;

    // Bot variables
    private int numOfBots;
    private int numOfTotalBots;
    private int numOfBotsDie = 0;

    private void Start()
    {
        // Get level
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        if (data.level >= levelDatas.Count)
        {
            data.level = 0;
            PlayerDataController.Ins.SaveToJson(data);
        }
        level = data.level;

        // Init plane
        gamePlane = Instantiate(levelDatas[level].gamePlane, planeHolder);

        // Init bot variables
        numOfBotsDie = 0;
        numOfTotalBots = levelDatas[level].numOfBots;
        numOfBots = numOfTotalBots;
    }

    #region Bot variables
    public void DecreaseNumOfBots(int decreaseNum)
    {
        numOfBots -= decreaseNum;
        numOfBotsDie += decreaseNum;
    }

    public int GetRemainNumOfBots()
    {
        return numOfBots;
    }

    public int GetNumOfBotsDie()
    {
        return numOfBotsDie;
    }

    public int GetNumOfTotalBots()
    {
        return numOfTotalBots;
    }
    #endregion

    #region Game State
    public Constant.GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState(Constant.GameState newGameState)
    {
        gameState = newGameState;
    }
    #endregion

    #region Game Function
    public void Win()
    {
        gameState = Constant.GameState.END;
    }

    public void Lose(Character killer)
    {
        gameState = Constant.GameState.END;
        finalKiller = killer;
    }

    public void StartGame(int level)
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        data.level = level;
        PlayerDataController.Ins.SaveToJson(data);

        SimplePool.ReleaseAll();
        player.playerSkin.CheckUnlockOneTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        SimplePool.ReleaseAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    public Character GetFinalKiller()
    {
        return finalKiller;
    }

    public LevelData GetLevelData(int level)
    {
        if (level >= levelDatas.Count)
        {
            level = 0;
        }
        return levelDatas[level];
    }
}
