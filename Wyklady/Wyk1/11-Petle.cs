// Wykład 1, slajd: Pętle
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

for (int i = 0; i < 10; i++)       // 10 monet
    Instantiate(coin, RandomPoint(), Quaternion.identity);

int tries = 0;
while (IsOccupied(point) && tries < 100)
{
    point = RandomPoint();
    tries++;
}

do code = Random.Range(1000, 10000); // min. raz
while (used.Contains(code));

foreach (Transform child in transform)
{
    if (!child.gameObject.activeSelf) continue;
    if (child.CompareTag("Boss")) break;
}
