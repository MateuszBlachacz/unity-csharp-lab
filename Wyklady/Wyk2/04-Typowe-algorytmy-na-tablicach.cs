// Wykład 2, slajd: Typowe algorytmy na tablicach
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int Sum(int[] t)
{
    int s = 0;
    foreach (int v in t) s += v;
    return s;
}
int IndexOfMin(float[] d)            // indeks najmniejszego
{
    int best = 0;
    for (int i = 1; i < d.Length; i++)
        if (d[i] < d[best]) best = i;
    return best;
}
int CountAbove(int[] t, int limit)
{
    int count = 0;
    foreach (int v in t) if (v > limit) count++;
    return count;
}
