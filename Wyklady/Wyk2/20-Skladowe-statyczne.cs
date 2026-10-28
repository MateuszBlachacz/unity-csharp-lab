// Wykład 2, slajd: Składowe statyczne
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class GameStats
{
    public static int TotalCoins;              // jedno dla całej gry
    public const int MaxLevel = 10;            // stała jest statyczna

    public static float Percent(int part, int all)
        => all == 0 ? 0f : 100f * part / all;
}

GameStats.TotalCoins += 5;                     // przez nazwę klasy
float p = GameStats.Percent(3, 4);             // 75

// znane przykłady
float d = Vector3.Distance(a, b);
float c = Mathf.Clamp(v, 0f, 1f);
float t = Time.deltaTime;
