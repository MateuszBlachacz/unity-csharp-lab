// Wyk3, slajd „Typy zagnieżdżone i klasy statyczne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Klasa statyczna: same składowe statyczne, obiektu nie da się utworzyć (new ArenaMath() to błąd kompilacji).
    public static class ArenaMath
    {
        public const float CellSize = 2f;

        public static Vector3 GridToWorld(int x, int y)
        {
            return new Vector3(x * CellSize, 0f, y * CellSize);
        }
    }
}
