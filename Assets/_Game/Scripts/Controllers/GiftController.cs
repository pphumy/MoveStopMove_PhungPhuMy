using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiftController : Singleton<GiftController>
{
    public Player player;
    public float spawnRaius;
    public float spawnTime;

    public GameObject giftPrefab;
    public Transform giftHolder;

    private void Awake()
    {
        SimplePool.Preload(giftPrefab, 3, giftHolder);
    }

    private void Start()
    {
        SpawnGift();
        StartCoroutine(IESpawnGift(spawnTime));
    }

    public void SpawnGift()
    {
        SimplePool.Spawn(giftPrefab, RandomNavMeshLocation(), Quaternion.identity);
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * spawnRaius;

        randomPosition += player.transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, spawnRaius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    IEnumerator IESpawnGift(float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if (LevelManager.Ins.GetGameState() == Constant.GameState.PLAY)
            {
                SpawnGift();
            }
        }
    }
}
