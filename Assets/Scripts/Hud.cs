using TMPro;
using UnityEngine;

// Zadanie 3 (L5): wynik i czas w lewym górnym rogu, gratulacje na środku po wygranej.
public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text hudText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private GameScore score;

    private void Start()
    {
        winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Po wygranej zegar staje na czasie wygranej.
        float time = score.IsWon ? score.WinTime : Time.timeSinceLevelLoad;
        // D4 dopełnia wynik zerami do 4 cyfr, F1 to jedna cyfra po przecinku
        // (separator zależy od ustawień regionalnych systemu).
        // Zadanie 6 ★ (L6): liczba pocisków odczytana z właściwości statycznej klasy Projectile.
        hudText.text = $"Wynik: {score.GetScore():D4}   Czas: {time:F1} s   Pociski: {Projectile.AliveCount}";

        if (score.IsWon && !winText.gameObject.activeSelf)
        {
            winText.text = $"Gratulacje, {score.PlayerName}!";
            winText.gameObject.SetActive(true);
        }
    }
}
