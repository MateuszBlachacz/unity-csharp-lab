// Wyk4, slajd „Wyrażenia lambda i domknięcia” (pułapka domknięcia w pętli for)
using UnityEngine;
using UnityEngine.UI;

namespace Wyklady.Wyk4
{
    public class LevelButtons : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;
        // Włącz, żeby zobaczyć błąd: każdy przycisk przekaże i == 3 (wartość po pętli).
        [SerializeField] private bool showClosureBug = false;

        private void Start()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (showClosureBug)
                {
                    // Błąd: wszystkie lambdy dzielą jedną zmienną i.
                    buttons[i].onClick.AddListener(() => SelectLevel(i));
                }
                else
                {
                    int index = i; // kopia: każda lambda ma własną zmienną
                    buttons[i].onClick.AddListener(() => SelectLevel(index));
                }
            }
        }

        private void SelectLevel(int index)
        {
            Debug.Log($"SelectLevel({index}): wybrano poziom o indeksie {index}" +
                      (index < buttons.Length ? $" (przycisk „Poziom {index + 1}”)" : " – takiego przycisku nie ma, to pułapka domknięcia"));
        }
    }
}
