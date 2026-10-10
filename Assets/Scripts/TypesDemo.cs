using UnityEngine;

public class TypesDemo : MonoBehaviour
{
    private void Start()
    {
        // przewidywanie: 3
        // Dzielenie dwóch int jest całkowite, część ułamkowa jest odcinana.
        Debug.Log($"7 / 2 = {7 / 2}");

        // przewidywanie: 3.5
        // Jeden argument float wystarczy, żeby całe dzielenie było zmiennoprzecinkowe.
        Debug.Log($"7 / 2f = {7 / 2f}");

        // przewidywanie: 1
        // % zwraca resztę z dzielenia: 7 = 2 * 3 + 1.
        Debug.Log($"7 % 3 = {7 % 3}");

        // przewidywanie: 3
        // Rzutowanie na int obcina część ułamkową, a nie zaokrągla.
        Debug.Log($"(int)3.9f = {(int)3.9f}");

        // przewidywanie: 2
        // Przy końcówce .5 RoundToInt wybiera liczbę parzystą, więc 2.5 daje 2, a 3.5 daje 4.
        Debug.Log($"Mathf.RoundToInt(2.5f) = {Mathf.RoundToInt(2.5f)}");

        // przewidywanie: -2147483648
        // int ma 32 bity; po przekroczeniu maksimum wartość „zawija się” na minimum.
        // Stała int.MaxValue + 1 wpisana wprost dałaby błąd kompilacji, dlatego zmienna.
        int big = int.MaxValue;
        big = big + 1;
        Debug.Log($"int.MaxValue + 1 = {big}");

        // przewidywanie: zależy od zaokrągleń, może wyjść True
        // 0.1, 0.2 i 0.3 nie mają dokładnej postaci binarnej, więc wynik == zależy od tego,
        // jak zaokrąglą się poszczególne liczby. Dla float błędy mogą się akurat znieść.
        Debug.Log($"0.1f + 0.2f == 0.3f: {0.1f + 0.2f == 0.3f}");

        // przewidywanie: False
        // W double suma to 0.30000000000000004, czyli inna liczba niż 0.3.
        Debug.Log($"0.1 + 0.2 == 0.3: {0.1 + 0.2 == 0.3}");

        // przewidywanie: True
        // Approximately porównuje z tolerancją, dlatego tak porównujemy liczby zmiennoprzecinkowe.
        Debug.Log($"Mathf.Approximately(0.1f + 0.2f, 0.3f): {Mathf.Approximately(0.1f + 0.2f, 0.3f)}");

        // przewidywanie: 53
        // String + int to sklejanie tekstu: liczba 3 zamienia się na "3".
        Debug.Log($"\"5\" + 3 = {"5" + 3}");

        // przewidywanie: 66
        // char w działaniu arytmetycznym zamienia się na kod znaku ('A' = 65), wynik to int.
        Debug.Log($"'A' + 1 = {'A' + 1}");
    }
}
