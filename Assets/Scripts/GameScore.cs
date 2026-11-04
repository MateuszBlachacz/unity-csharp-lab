using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

// Wynik i warunek wygranej z L3. Liczba zebranych monet jest liczona tutaj,
// a liczbę utworzonych monet podaje CoinSpawner (L3–L4) albo LevelLoader (od L5).
public class GameScore : MonoBehaviour
{
    private const int MaxMultiplier = 3;

    // Zadanie 7 ★: monety zebrane w odstępie krótszym niż comboWindow budują serię.
    [SerializeField] private float comboWindow = 1.5f;

    private int score;
    private int multiplier = 1;
    // Na start daleko w przeszłości, żeby pierwsza moneta nie liczyła się jako seria.
    private float lastPickupTime = -100f;
    private int coinsTotal;
    private int coinsCollected;
    private bool gameWon;
    // Zadanie 5 (L4): liczba zebranych monet każdego typu, indeks to (int)CoinType.
    private readonly int[] countByType = new int[System.Enum.GetValues(typeof(CoinType)).Length];
    // Suma wartości bazowych zebranych monet (bez mnożnika serii) do średniej.
    private int collectedValue;
    // Zadanie 5 (L5): dziennik zebranych monet, po jednej linii na monetę.
    private readonly StringBuilder log = new StringBuilder();
    private float winTime;

    public string PlayerName { get; private set; } = "Gracz";
    public bool IsWon => gameWon;
    // Czas gry w chwili wygranej (sekundy od wczytania sceny).
    public float WinTime => winTime;

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            LogCoinStats();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            PrintLog();
        }
    }

    public void SetPlayerName(string playerName)
    {
        PlayerName = playerName;
    }

    public void AddPoints(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning($"Pominięto ujemną liczbę punktów: {amount}");
            return;
        }

        score += amount;
        Debug.Log($"Wynik: {score}");
    }

    public void AddPoints(int amount, string reason)
    {
        Debug.Log($"+{amount} ({reason})");
        AddPoints(amount);
    }

    public void AddCoinPoints(int value)
    {
        multiplier = NextMultiplier(Time.time - lastPickupTime);
        lastPickupTime = Time.time;
        AddPoints(value * multiplier, $"seria x{multiplier}");
    }

    // Tylko liczy nowy mnożnik, niczego nie zmienia ani nie wypisuje.
    private int NextMultiplier(float timeSinceLast)
    {
        return timeSinceLast < comboWindow ? Mathf.Min(multiplier + 1, MaxMultiplier) : 1;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetCoinTotal(int total)
    {
        coinsTotal = total;
    }

    public void CoinCollected(Coin coin)
    {
        coinsCollected++;
        countByType[(int)coin.Type]++;
        collectedValue += coin.GetValue();
        log.AppendLine($"[{Time.time:F2}] {coin.Type} +{coin.GetValue()}");

        // Flaga gameWon gwarantuje jeden komunikat, nawet gdy dwie ostatnie monety
        // zostaną zebrane w tej samej klatce.
        if (!gameWon && coinsTotal > 0 && coinsCollected >= coinsTotal)
        {
            gameWon = true;
            winTime = Time.timeSinceLevelLoad;
            float time = winTime;
            char rank = GetRank(time);
            // Najpierw premia, potem wynik do komunikatu.
            AddPoints(GetRankBonus(rank), $"ranga {rank}");
            Debug.Log($"Wygrana, {PlayerName}! Zebrano wszystkie monety ({coinsTotal}) w {time:F1} s, ranga {rank}, wynik: {GetScore()}");
        }
    }

    // Zadanie 8 ★: gałęzie sprawdzane od góry, więc kolejność progów ma znaczenie.
    private char GetRank(float time) => time switch
    {
        < 20f => 'S',
        < 35f => 'A',
        < 60f => 'B',
        _ => 'C'
    };

    private int GetRankBonus(char rank)
    {
        switch (rank)
        {
            case 'S':
                return 50;
            case 'A':
                return 25;
            case 'B':
                return 10;
            default:
                return 0;
        }
    }

    private void LogCoinStats()
    {
        int total = 0;
        for (int i = 0; i < countByType.Length; i++)
        {
            Debug.Log($"{(CoinType)i}: {countByType[i]}");
            total += countByType[i];
        }

        // Dzielenie przez float, bo int / int obcięłoby część ułamkową.
        string average = total > 0 ? $"{collectedValue / (float)total:F2}" : "brak";
        Debug.Log($"Razem monet: {total} (licznik zebranych: {coinsCollected}), średnia wartość: {average}");
    }

    private void PrintLog()
    {
        string text = log.ToString();
        int goldCount = 0;
        // Ostatnia linia kończy się znakiem nowej linii, więc puste elementy pomijamy.
        foreach (string line in text.Split('\n', System.StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.Contains("Gold"))
            {
                goldCount++;
            }
        }

        Debug.Log($"Dziennik ({coinsCollected} monet):\n{text}Wpisów ze słowem Gold: {goldCount}");
    }
}
