// Wykład 2, slajd: Parametry ref i out
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

void Swap(ref int a, ref int b)
    { int tmp = a; a = b; b = tmp; }

bool TryFindSpawn(out Vector3 point)
{
    foreach (Transform p in spawnPoints)
        if (!IsOccupied(p.position))
        {
            point = p.position;  // out musi być przypisany
            return true;
        }
    point = Vector3.zero;
    return false;
}
int x = 1, y = 2;
Swap(ref x, ref y);              // x = 2, y = 1
if (TryFindSpawn(out Vector3 p)) Spawn(p);
if (int.TryParse(text, out int n)) score = n;
