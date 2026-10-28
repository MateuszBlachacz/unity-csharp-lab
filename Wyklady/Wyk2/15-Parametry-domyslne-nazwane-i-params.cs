// Wykład 2, slajd: Parametry domyślne, nazwane i params
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

void Spawn(GameObject prefab, int count = 1,
           float radius = 5f)
{
    for (int i = 0; i < count; i++)
        Instantiate(prefab, RandomPoint(radius),
                    Quaternion.identity);
}

Spawn(coin);                     // count = 1, radius = 5
Spawn(coin, 10);                 // radius = 5
Spawn(coin, radius: 2f);         // argument nazwany

int Sum(params int[] values)
{
    int s = 0;
    foreach (int v in values) s += v;
    return s;
}
int total = Sum(1, 2, 3, 4);     // dowolna liczba argumentów
