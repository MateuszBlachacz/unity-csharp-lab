// Wykład 2, slajd: Przechodzenie po tablicy: for i foreach
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int[] coins = { 3, 0, 5, 2 };

for (int i = 0; i < coins.Length; i++)
{
    coins[i] *= 2;                  // można zmieniać elementy
    Debug.Log($"Pole {i}: {coins[i]}");
}

int sum = 0;
foreach (int c in coins)            // tylko odczyt
{
    sum += c;
    // c = 0;   // błąd: zmienna iteracji jest tylko do odczytu
}

for (int i = coins.Length - 1; i >= 0; i--)
    Debug.Log(coins[i]);           // od końca
