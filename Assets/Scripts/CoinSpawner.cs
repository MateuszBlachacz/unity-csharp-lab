using UnityEngine;

// Losowe rozmieszczenie monet z L3.
public class CoinSpawner : MonoBehaviour
{
    private const int MaxAttempts = 20;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private GameScore score;
    [SerializeField] private int count = 10;
    [SerializeField] private float minDistanceFromPlayer = 2f;
    // Te same granice co ruch gracza w L2.
    [SerializeField] private Vector2 arenaHalfSize = new Vector2(8.5f, 4.5f);
    [SerializeField] private float coinHeight = 0.8f;

    private int spawnedCount;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            if (!TryFindPosition(out Vector3 pos))
            {
                Debug.LogWarning($"Moneta {i + 1}: brak wolnego miejsca po {MaxAttempts} próbach, pomijam ją.");
                continue;
            }

            GameObject coinObject = Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, transform);
            coinObject.GetComponent<Coin>().SetType(RollCoinType());
            spawnedCount++;
        }

        score.SetCoinTotal(spawnedCount);
        Debug.Log($"Utworzono monet: {spawnedCount} z {count}");
    }

    private bool TryFindPosition(out Vector3 pos)
    {
        int attempts = 0;
        do
        {
            pos = new Vector3(Random.Range(-arenaHalfSize.x, arenaHalfSize.x), coinHeight,
                              Random.Range(-arenaHalfSize.y, arenaHalfSize.y));
            attempts++;
        } while (IsTooCloseToPlayer(pos) && attempts < MaxAttempts);

        return !IsTooCloseToPlayer(pos);
    }

    private bool IsTooCloseToPlayer(Vector3 pos)
    {
        Vector3 playerPos = player.position;
        playerPos.y = pos.y;   // odległość liczymy w poziomie
        return Vector3.Distance(pos, playerPos) < minDistanceFromPlayer;
    }

    // Zadanie 6 ★: brązowe 60%, srebrne 30%, złote 10%.
    private CoinType RollCoinType()
    {
        float r = Random.value;
        if (r < 0.6f)
        {
            return CoinType.Bronze;
        }
        if (r < 0.9f)
        {
            return CoinType.Silver;
        }
        return CoinType.Gold;
    }

    [ContextMenu("Test losowania 1000 typów")]
    private void TestRoll()
    {
        int bronze = 0;
        int silver = 0;
        int gold = 0;
        for (int i = 0; i < 1000; i++)
        {
            switch (RollCoinType())
            {
                case CoinType.Bronze:
                    bronze++;
                    break;
                case CoinType.Silver:
                    silver++;
                    break;
                case CoinType.Gold:
                    gold++;
                    break;
            }
        }
        Debug.Log($"1000 losowań: brązowe {bronze}, srebrne {silver}, złote {gold}");
    }
}
