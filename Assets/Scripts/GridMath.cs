using UnityEngine;

// Przeliczanie komórki mapy (wiersz, kolumna) na pozycję w świecie, wspólne dla
// ArenaBuilder (L4) i LevelLoader (L5).
public static class GridMath
{
    // Wiersz rośnie „w dół” mapy, a Z w głąb sceny, dlatego Z liczymy z odwróceniem.
    // Odjęcie (n - 1) / 2 wyśrodkowuje mapę względem punktu (0, 0, 0).
    public static Vector3 CellToWorld(int row, int col, int rows, int cols, float cellSize, float y)
    {
        float x = (col - (cols - 1) / 2f) * cellSize;
        float z = ((rows - 1) / 2f - row) * cellSize;
        return new Vector3(x, y, z);
    }
}
