using UnityEngine;

// Zadanie 2 (L4): ściany areny budowane na starcie z tablicy dwuwymiarowej.
// 1 to ściana, 0 puste pole. Pierwszy wiersz tablicy to najdalszy rząd areny (największe Z).
public class ArenaBuilder : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform wallsParent;
    [SerializeField] private float cellSize = 1f;
    // Środek sześcianu 1 × 1 × 1 stojącego na podłodze.
    [SerializeField] private float wallHeight = 0.5f;

    // Inspector nie wyświetla tablic int[,], dlatego mapa jest zapisana w kodzie.
    private readonly int[,] map =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };

    private void Start()
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        int built = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (map[row, col] != 1)
                {
                    continue;
                }

                Instantiate(wallPrefab, CellToWorld(row, col, rows, cols), Quaternion.identity, wallsParent);
                built++;
            }
        }

        Debug.Log($"Arena {rows} × {cols}: utworzono ścian: {built}");
    }

    // Wiersz rośnie „w dół” mapy, a Z w głąb sceny, dlatego Z liczymy z odwróceniem.
    // Odjęcie (n - 1) / 2 wyśrodkowuje arenę względem punktu (0, 0, 0).
    private Vector3 CellToWorld(int row, int col, int rows, int cols)
    {
        float x = (col - (cols - 1) / 2f) * cellSize;
        float z = ((rows - 1) / 2f - row) * cellSize;
        return new Vector3(x, wallHeight, z);
    }
}
