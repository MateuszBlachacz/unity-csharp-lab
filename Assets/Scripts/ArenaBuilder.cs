using UnityEngine;

// Zadanie 2 (L4): ściany areny budowane na starcie z tablicy dwuwymiarowej.
// 1 to ściana, 0 puste pole. Pierwszy wiersz tablicy to najdalszy rząd areny (największe Z).
// Zadanie 6 ★: drugi poziom w tablicy postrzępionej, wybierany w Inspectorze.
public class ArenaBuilder : MonoBehaviour
{
    public enum LevelChoice { Level1, Level2 }

    [SerializeField] private LevelChoice level = LevelChoice.Level1;
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

    // Zadanie 6 ★: arena w kształcie litery L. Wiersze mają różną długość,
    // a brakujących komórek po prawej stronie górnych wierszy po prostu nie ma.
    private readonly int[][] level2 =
    {
        new[] { 1, 1, 1, 1, 1, 1, 1, 1 },
        new[] { 1, 0, 0, 0, 0, 0, 0, 1 },
        new[] { 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1 },
        new[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        new[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        new[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };

    private void Start()
    {
        int rows = RowCount();
        int cols = ColumnCount();
        int built = 0;

        for (int row = 0; row < rows; row++)
        {
            // W tablicy postrzępionej każdy wiersz może mieć inną długość.
            int rowLength = RowLength(row);
            for (int col = 0; col < rowLength; col++)
            {
                if (Cell(row, col) != 1)
                {
                    continue;
                }

                Instantiate(wallPrefab, CellToWorld(row, col, rows, cols), Quaternion.identity, wallsParent);
                built++;
            }
        }

        Debug.Log($"{level}: arena {rows} × {cols}, utworzono ścian: {built}");
    }

    // Zadanie 6 ★: czy punkt świata leży na pustym polu wybranego poziomu.
    // Pole poza mapą (np. poza krótszym wierszem) też nie jest wolne.
    public bool IsFreeCell(Vector3 worldPos)
    {
        int rows = RowCount();
        int cols = ColumnCount();
        int col = Mathf.RoundToInt(worldPos.x / cellSize + (cols - 1) / 2f);
        int row = Mathf.RoundToInt((rows - 1) / 2f - worldPos.z / cellSize);

        if (row < 0 || row >= rows || col < 0 || col >= RowLength(row))
        {
            return false;
        }
        return Cell(row, col) == 0;
    }

    // Poniższe metody ukrywają różnicę między int[,] a int[][].
    private int RowCount() => level == LevelChoice.Level1 ? map.GetLength(0) : level2.Length;

    private int RowLength(int row) => level == LevelChoice.Level1 ? map.GetLength(1) : level2[row].Length;

    private int Cell(int row, int col) => level == LevelChoice.Level1 ? map[row, col] : level2[row][col];

    // Szerokość areny do wyśrodkowania: najdłuższy wiersz.
    private int ColumnCount()
    {
        int max = 0;
        for (int row = 0; row < RowCount(); row++)
        {
            max = Mathf.Max(max, RowLength(row));
        }
        return max;
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
