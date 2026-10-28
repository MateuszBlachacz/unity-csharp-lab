// Wykład 2, slajd: Krotki: kilka wyników bez out
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

(int min, int max) MinMax(int[] values)
{
    int lo = values[0], hi = values[0];
    foreach (int v in values)
    {
        if (v < lo) lo = v;
        if (v > hi) hi = v;
    }
    return (lo, hi);
}

var r = MinMax(scores);
Debug.Log($"Najgorszy {r.min}, najlepszy {r.max}");

(int worst, int best) = MinMax(scores);   // dekonstrukcja
(a, b) = (b, a);                          // zamiana bez tmp
