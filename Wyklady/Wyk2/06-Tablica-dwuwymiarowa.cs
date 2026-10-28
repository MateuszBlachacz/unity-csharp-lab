// Wykład 2, slajd: Tablica dwuwymiarowa
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int[,] map =
{
    { 1, 1, 1, 1, 1 },
    { 1, 0, 0, 2, 1 },
    { 1, 0, 1, 0, 1 },
    { 1, 1, 1, 1, 1 },
};
int rows = map.GetLength(0);   // 4
int cols = map.GetLength(1);   // 5

for (int r = 0; r < rows; r++)
    for (int c = 0; c < cols; c++)
        if (map[r, c] == 1)
            Instantiate(wall, new Vector3(c, 0.5f, -r),
                        Quaternion.identity);
