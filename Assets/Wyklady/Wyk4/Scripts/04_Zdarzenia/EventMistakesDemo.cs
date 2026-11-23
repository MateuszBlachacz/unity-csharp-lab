// Wyk4, slajd „Zdarzenia: najczęstsze błędy”
using System;
using UnityEngine;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-20)]
    public class EventMistakesDemo : MonoBehaviour
    {
        private event Action Fired;

        private int Count => Fired == null ? 0 : Fired.GetInvocationList().Length;

        private void Start()
        {
            Debug.Log("=== Wyk4, część 4: zdarzenia ===");
            Debug.Log("--- Zdarzenia: najczęstsze błędy ---");

            // Błąd 1: wywołanie bez ?. – gdy nikt nie słucha, zdarzenie jest null.
            // Fired.Invoke(); // NullReferenceException
            Fired?.Invoke(); // bezpiecznie: nic się nie dzieje
            Debug.Log($"Fired?.Invoke() bez słuchaczy: brak wyjątku, metod {Count}");

            // Błąd 2: wypisywanie lambdy napisanej drugi raz.
            Fired += () => Debug.Log("Lambda");
            Debug.Log($"Po += lambda: metod {Count}");
            Fired -= () => Debug.Log("Lambda"); // nowa lambda = inny obiekt, nic nie zostaje wypisane
            Debug.Log($"Po -= takiej samej lambdy: metod {Count} (bez zmian)");

            // Poprawnie: zapamiętać referencję i wypisać tę samą.
            Action handler = () => Debug.Log("Handler");
            Fired += handler;
            Debug.Log($"Po += handler: metod {Count}");
            Fired -= handler;
            Debug.Log($"Po -= handler: metod {Count}");

            // Pozostałe błędy z listy:
            // - zapis w OnEnable bez wypisania w OnDisable: zniszczony obiekt zostaje na liście słuchaczy
            //   (MissingReferenceException albo wyciek pamięci);
            // - zapis w Start, a wypis w OnDisable: po ponownym włączeniu obiektu słuchacz już nie wraca;
            // - podwójne += tej samej metody: metoda wywoła się dwa razy;
            // - statyczne zdarzenia bez czyszczenia: przeżywają zmianę sceny (patrz GameEvents.ResetEvents);
            // - zdarzenie jako publiczne pole delegatu bez słowa event: każdy może je wywołać albo nadpisać przez =.
        }
    }
}
