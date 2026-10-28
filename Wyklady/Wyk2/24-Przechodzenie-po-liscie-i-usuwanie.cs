// Wykład 2, slajd: Przechodzenie po liście i usuwanie
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

var coins = new List<int> { 10, 20, 30, 40 };

for (int i = 0; i < coins.Count; i++)
    coins[i] *= 2;                    // 20, 40, 60, 80

foreach (int c in coins)
    Debug.Log(c);

// foreach (int c in coins)
//     if (c > 50) coins.Remove(c);   // InvalidOperationException

for (int i = coins.Count - 1; i >= 0; i--)
    if (coins[i] > 50) coins.RemoveAt(i);   // 20, 40
