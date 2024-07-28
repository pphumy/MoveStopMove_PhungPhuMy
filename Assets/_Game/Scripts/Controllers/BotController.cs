using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : Singleton<BotController>
{
    [Header("PlayerInfor")]
    public Player player;

    [Header("BotSpawner")]
    public int numOfBotsOnGround;
    public Transform botsHolder;
    public CharacterBoundary botPrefab;

    public float reviveTime;

    public float xRange;
    public float zRange;

    public Transform indicatorsHolder;
    public Indicator indicatorPrefab;

    private string[] names = {
        "Nanda3",
        "Kumar1407",
        "MeBuffalo",
        "###Hello###",
        "GoodToKnow",
        "ItsMe999",
        "JustPlay",
        "Slayder",
        "beBee",
        "okIm5",
        "jetLaggg",
        "3toCount",
        "normalPlayer",
        "imABot",
        "lovePizza",
        "humanOidx",
        "randomName",
        "justGuess",
        "newName"
    };

    private List<string> namesToUse = new List<string>();

    private void Awake()
    {
        for (int i = 0; i < numOfBotsOnGround; i++)
        {
            SimplePool.Preload(botPrefab.gameObject, numOfBotsOnGround, botsHolder);
            SimplePool.Preload(indicatorPrefab.gameObject, numOfBotsOnGround, indicatorsHolder);
        }
    }

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            namesToUse.Add(names[i]);
        }

        SpawnAllBots();
    }

    public void PreloadBots()
    {
        SimplePool.ReleaseAll();
        for (int i = 0; i < numOfBotsOnGround; i++)
        {
            SimplePool.Preload(botPrefab.gameObject, numOfBotsOnGround, botsHolder);
            SimplePool.Preload(indicatorPrefab.gameObject, numOfBotsOnGround, indicatorsHolder);
        }
    }

    public void SpawnAllBots()
    {
        SimplePool.CollectAPool(botPrefab.gameObject);
        SimplePool.CollectAPool(indicatorPrefab.gameObject);

        for (int i = 0; i < numOfBotsOnGround; i++)
        {
            SpawnBot();
        }
    }

    public void ClearBot()
    {
        LevelManager.Ins.DecreaseNumOfBots(1);
    }

    public void ReuseBot(GameObject bot)
    {
        // Despawn
        SimplePool.Despawn(bot);
        SimplePool.Despawn(Cache.Ins.GetIndicatorGOFromBotGO(bot));

        // Check if need to spawn
        int remainNumOfBots = LevelManager.Ins.GetRemainNumOfBots();
        int numActiveBots = SimplePool.GetNumOfActiveObjs(botPrefab.gameObject);

        if (remainNumOfBots >= numOfBotsOnGround)
        {
            SpawnBot();
        }
        else
        {
            if (numActiveBots < remainNumOfBots)
            {
                for (int i = 0; i < remainNumOfBots - numActiveBots; i++)
                {
                    SpawnBot();
                }
            }
        }
    }

    private void SpawnBot()
    {
        Vector3 randomPos = GetRandomPos();
        Quaternion randomRot = GetRandomRot();

        // Init bot
        GameObject botGO = SimplePool.Spawn(botPrefab.gameObject, randomPos, randomRot);
        Character botChar = botGO.GetComponent<CharacterBoundary>().character;
        string botName = namesToUse[Random.Range(0, namesToUse.Count)];
        botChar.SetName(botName);
        namesToUse.Remove(botName);

        // Set indicator GO
        GameObject indicatorGO = SimplePool.Spawn(indicatorPrefab.gameObject, randomPos, Quaternion.identity);
        indicatorGO.transform.SetParent(indicatorsHolder);
        indicatorGO.transform.localScale = Vector3.one;

        // Set indicator
        Indicator indicator = Cache.Ins.GetIndicatorFromGameObj(indicatorGO);
        indicator.SetOriginCharacter(botChar);
        indicator.SetMaterial();
        indicator.SetName();
        Cache.Ins.SetBotGOToIndicatorGO(botGO, indicatorGO);

        // Set bot variables
        float botScale = Random.Range(0.9f, 1.1f);
        botGO.transform.localScale = Vector3.one;
        botChar.IncreaseScale(player.GetScale() * botScale);

        // Set scale and score to match player
        if (botScale < 1)
        {
            if (player.GetScore() <= 2)
            {
                botChar.SetScore(1);
            }
            else
            {
                botChar.SetScore(player.GetScore() - Random.Range(1, 2));
            }
        }
        else
        {
            botChar.SetScore(player.GetScore() + Random.Range(1, 2));
        }
    }

    private Vector3 GetRandomPos()
    {
        // 0 is TOP, 1 is BOT, 2 is LEFT, 3 is RIGHT
        float randomX = 0;
        float randomZ = 0;

        int randomPos = Random.Range(0, 3);
        switch (randomPos)
        {
            case 0:
                randomX = xRange;
                randomZ = Random.Range(-zRange, zRange);
                break;
            case 1:
                randomX = -xRange;
                randomZ = Random.Range(-zRange, zRange);
                break;
            case 2:
                randomZ = -zRange;
                randomX = Random.Range(-xRange, xRange);
                break;
            case 3:
                randomZ = zRange;
                randomX = Random.Range(-xRange, xRange);
                break;

            default:
                break;
        }

        return new Vector3(randomX, 0, randomZ);
    }

    private Quaternion GetRandomRot()
    {
        return Quaternion.Euler(0, 60, 0);
    }
}
