using UnityEngine;

// Monety w losowo wybranych punktach spawnu (L4). Typ monety wynika z wybranego prefabu.
public class CoinSpawner : MonoBehaviour
{
    // Puste obiekty na wolnych polach areny.
    [SerializeField] private Transform[] spawnPoints;
    // Kolejność jak w CoinType: brązowa, srebrna, złota.
    [SerializeField] private GameObject[] coinPrefabs;
    [SerializeField] private GameScore score;
    [SerializeField] private int count = 5;

    private void Start()
    {
        int toSpawn = count;
        if (toSpawn > spawnPoints.Length)
        {
            Debug.LogWarning($"Za mało punktów spawnu: {spawnPoints.Length} na {count} monet. Tworzę {spawnPoints.Length}.");
            toSpawn = spawnPoints.Length;
        }

        int[] order = ShuffledIndices(spawnPoints.Length);
        int spawnedCount = 0;

        // Pierwsze toSpawn indeksów z przetasowanej tablicy to różne punkty spawnu.
        for (int i = 0; i < toSpawn; i++)
        {
            Transform point = spawnPoints[order[i]];
            GameObject prefab = coinPrefabs[RollPrefabIndex()];
            Instantiate(prefab, point.position, prefab.transform.rotation, transform);
            spawnedCount++;
        }

        score.SetCoinTotal(spawnedCount);
        Debug.Log($"Utworzono monet: {spawnedCount} z {count}");
    }

    // Zadanie 3 (L4): tablica indeksów 0..n-1 przetasowana algorytmem Fishera–Yatesa.
    private int[] ShuffledIndices(int n)
    {
        int[] order = new int[n];
        for (int i = 0; i < order.Length; i++)
        {
            order[i] = i;
        }

        for (int i = order.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);   // górna granica wyłączna, więc j może być równe i
            (order[i], order[j]) = (order[j], order[i]);
        }

        return order;
    }

    // Gdy prefabów jest tyle, ile typów monet, zostaje losowanie z wagami z L3;
    // przy innej liczbie każdy prefab ma tę samą szansę.
    private int RollPrefabIndex()
    {
        if (coinPrefabs.Length == System.Enum.GetValues(typeof(CoinType)).Length)
        {
            return (int)RollCoinType();
        }
        return Random.Range(0, coinPrefabs.Length);
    }

    // Zadanie 6 ★ z L3: brązowe 60%, srebrne 30%, złote 10%.
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
