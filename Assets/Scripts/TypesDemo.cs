using UnityEngine;

// Zadania 1 i 2 z L2: przewidywanie wyników wyrażeń i dobór typów.
public class TypesDemo : MonoBehaviour
{
    private const int MaxLives = 5;
    private readonly System.DateTime createdAt = System.DateTime.Now;

    private void Start()
    {
        // Tabela A. Arytmetyka i rzutowanie

        // przewidywanie: 3; int / int daje int, część ułamkowa jest odrzucana
        Debug.Log($"7 / 2 = {7 / 2}");
        // przewidywanie: 3.5; jeden argument float, więc dzielenie zmiennoprzecinkowe
        Debug.Log($"7 / 2f = {7 / 2f}");
        // przewidywanie: 1; reszta z dzielenia 7 przez 3
        Debug.Log($"7 % 3 = {7 % 3}");
        // przewidywanie: -3; dzielenie całkowite obcina w stronę zera, a nie w dół
        Debug.Log($"-7 / 2 = {-7 / 2}");
        // przewidywanie: -1; reszta ma znak dzielnej
        Debug.Log($"-7 % 3 = {-7 % 3}");
        // przewidywanie: 8; najpierw 10 / 4 = 2 (int), potem 2 * 4
        Debug.Log($"10 / 4 * 4 = {10 / 4 * 4}");
        // przewidywanie: 10; 10 / 4f = 2.5, potem 2.5 * 4
        Debug.Log($"10 / 4f * 4 = {10 / 4f * 4}");
        // przewidywanie: 3; rzutowanie następuje po dzieleniu całkowitym, ułamek już zniknął
        Debug.Log($"(float)(7 / 2) = {(float)(7 / 2)}");
        // przewidywanie: 3; rzutowanie na int obcina, a nie zaokrągla
        Debug.Log($"(int)3.9f = {(int)3.9f}");
        // przewidywanie: -3; obcięcie w stronę zera
        Debug.Log($"(int)-3.9f = {(int)-3.9f}");
        // przewidywanie: -4; FloorToInt zaokrągla w dół, czyli w stronę minus nieskończoności
        Debug.Log($"Mathf.FloorToInt(-3.9f) = {Mathf.FloorToInt(-3.9f)}");
        // przewidywanie: 2; wartości x,5 są zaokrąglane do najbliższej liczby parzystej
        Debug.Log($"Mathf.RoundToInt(2.5f) = {Mathf.RoundToInt(2.5f)}");
        // przewidywanie: 4; najbliższa parzysta to 4
        Debug.Log($"Mathf.RoundToInt(3.5f) = {Mathf.RoundToInt(3.5f)}");
        // przewidywanie: i = 7, j = 12; i++ zwraca 5 (i staje się 6), ++i zwiększa do 7 i zwraca 7
        int i = 5;
        int j = i++ + ++i;
        Debug.Log($"int i = 5; int j = i++ + ++i; daje i = {i}, j = {j}");

        // Tabela B. Tekst, znaki i granice typów

        // przewidywanie: "53"; string + int łączy tekst
        Debug.Log($"\"5\" + 3 = {"5" + 3}");
        // przewidywanie: "535"; od lewej: "5" + 3 = "53", potem "53" + 5
        Debug.Log($"\"5\" + 3 + 5 = {"5" + 3 + 5}");
        // przewidywanie: "85"; od lewej: 5 + 3 = 8 (int), potem 8 + "5"
        Debug.Log($"5 + 3 + \"5\" = {5 + 3 + "5"}");
        // przewidywanie: 66; char w działaniu arytmetycznym zamienia się na int (kod 'A' to 65)
        Debug.Log($"'A' + 1 = {'A' + 1}");
        // przewidywanie: B; kod 66 rzutowany z powrotem na znak
        Debug.Log($"(char)('A' + 1) = {(char)('A' + 1)}");
        // przewidywanie: -2147483648; przepełnienie bez sprawdzania „zawija” wartość
        int big = int.MaxValue;
        big = big + 1;
        Debug.Log($"int.MaxValue + 1 = {big}");
        // przewidywanie: 0; byte ma zakres 0–255, po 255 wraca do 0
        byte b = 255;
        b++;
        Debug.Log($"byte b = 255; b++; daje b = {b}");
        // przewidywanie: nieskończoność; dzielenie float przez zero nie zgłasza błędu
        Debug.Log($"1f / 0f = {1f / 0f}");
        // przewidywanie: False; float ma ograniczoną precyzję, więc wynik zależy od zaokrągleń
        // i może wyjść True; faktyczny wynik odczytaj w konsoli.
        Debug.Log($"0.1f + 0.2f == 0.3f: {0.1f + 0.2f == 0.3f}");
        Debug.Log($"Mathf.Approximately(0.1f + 0.2f, 0.3f): {Mathf.Approximately(0.1f + 0.2f, 0.3f)}");
        // przewidywanie: False; w double 0.1 + 0.2 to 0.30000000000000004
        Debug.Log($"0.1 + 0.2 == 0.3: {0.1 + 0.2 == 0.3}");

        ShowTypes();

        // Próby zmiany stałej i pola readonly (usunięte, bo się nie kompilują):
        // MaxLives = 6;               błąd: do stałej nie można niczego przypisać, jej wartość ustala kompilator
        // createdAt = System.DateTime.Now;   błąd: pole readonly można ustawić tylko przy deklaracji
        //                                    albo w konstruktorze
        // var level = 1;
        // level = "drugi";            błąd: var nie jest typem dynamicznym; level ma typ int ustalony
        //                             przy kompilacji, a tekstu nie da się zamienić niejawnie na int
    }

    private void ShowTypes()
    {
        int lives = 3;                      // liczba całkowita, mały zakres
        float speed = 5.5f;                 // Unity liczy pozycje i prędkości we float
        string playerName = "Ola";          // tekst
        bool isAlive = true;                // tylko dwie wartości
        char actionKey = 'E';               // jeden znak
        long rankingPoints = 10_000_000_000L;   // 10 mld nie mieści się w int (ok. ±2,1 mld)
        decimal price = 19.99m;             // dokładne ułamki dziesiętne, dobre do pieniędzy
        Vector3 coinPosition = new Vector3(-5f, 0.8f, 2f);   // punkt w przestrzeni 3D
        Color coinColor = new Color(1f, 0.8f, 0.1f);         // kolor RGB w zakresie 0–1

        Debug.Log($"Życia: {lives}, prędkość: {speed}, imię: {playerName}, żyje: {isAlive}, klawisz: {actionKey}, " +
                  $"ranking: {rankingPoints}, cena: {price}, moneta: {coinPosition}, kolor: {coinColor}");
        Debug.Log($"MaxLives = {MaxLives}, createdAt = {createdAt}");
    }
}
