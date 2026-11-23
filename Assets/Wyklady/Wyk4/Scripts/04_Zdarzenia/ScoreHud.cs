// Wyk4, omówienie zadania projektowego „Zdarzenia w Arenie” (HUD wyniku)
using TMPro;
using UnityEngine;

namespace Wyklady.Wyk4
{
    // HUD nie słucha monet ani zabójstw, tylko zmiany wyniku: nie musi wiedzieć,
    // skąd biorą się punkty, więc nowe źródło punktów nie wymaga zmian w HUD.
    public class ScoreHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        private void OnEnable()
        {
            GameEvents.ScoreChanged += Show;
        }

        private void OnDisable()
        {
            GameEvents.ScoreChanged -= Show;
        }

        private void Show(int score)
        {
            label.text = $"Wynik: {score}";
        }
    }
}
