// Wyk4, slajd „Rozgrzewka: dziedziczenie”
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Pierwszy w kolejności wykładu: wypisuje nagłówek części 1.
    [DefaultExecutionOrder(-60)]
    public class WarmupQuiz : MonoBehaviour
    {
        private class A
        {
            public virtual string F() => "A.F";
            public string G() => "A.G";
        }

        private class B : A
        {
            public override string F() => "B.F";
            public new string G() => "B.G";
        }

        private void Start()
        {
            Debug.Log("=== Wyk4, część 1: typy generyczne ===");
            Debug.Log("--- Rozgrzewka: dziedziczenie ---");

            A x = new B();
            // Oczekiwany wynik: B.F A.G
            // F jest virtual i nadpisana (override), więc wywołanie zależy od typu obiektu (B).
            // G w B jest ukryta słowem new, więc wywołanie zależy od typu zmiennej (A).
            Debug.Log(x.F() + " " + x.G());
        }
    }
}
