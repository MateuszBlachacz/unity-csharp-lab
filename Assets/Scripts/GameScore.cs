using UnityEngine;

// Wynik i warunek wygranej z L3. Liczba zebranych monet jest liczona tutaj,
// a liczbę utworzonych monet podaje CoinSpawner.
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

    public void CoinCollected()
    {
        coinsCollected++;

        // Flaga gameWon gwarantuje jeden komunikat, nawet gdy dwie ostatnie monety
        // zostaną zebrane w tej samej klatce.
        if (!gameWon && coinsTotal > 0 && coinsCollected >= coinsTotal)
        {
            gameWon = true;
            float time = Time.timeSinceLevelLoad;
            char rank = GetRank(time);
            // Najpierw premia, potem wynik do komunikatu.
            AddPoints(GetRankBonus(rank), $"ranga {rank}");
            Debug.Log($"Wygrana! Zebrano wszystkie monety ({coinsTotal}) w {time:F1} s, ranga {rank}, wynik: {GetScore()}");
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
}
