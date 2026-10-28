using UnityEngine;

// Wynik i warunek wygranej z L3. Liczba zebranych monet jest liczona tutaj,
// a liczbę utworzonych monet podaje CoinSpawner.
public class GameScore : MonoBehaviour
{
    private int score;
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
            Debug.Log($"Wygrana! Zebrano wszystkie monety ({coinsTotal}) w {Time.timeSinceLevelLoad:F1} s, wynik: {score}");
        }
    }
}
