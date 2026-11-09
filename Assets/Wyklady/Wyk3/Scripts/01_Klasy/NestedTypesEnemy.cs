// Wyk3, slajd „Typy zagnieżdżone i klasy statyczne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class NestedTypesEnemy : MonoBehaviour
    {
        // Typy zagnieżdżone: poza klasą piszemy NestedTypesEnemy.State i NestedTypesEnemy.Loot.
        public enum State { Idle, Patrol, Chase }

        [System.Serializable]
        public class Loot
        {
            public string item = "Moneta";
            [Range(0f, 1f)] public float chance = 0.5f;
        }

        [SerializeField] private State state = State.Patrol;
        [SerializeField] private Loot[] loot = { new Loot(), new Loot { item = "Apteczka", chance = 0.1f } };

        private void Start()
        {
            string first = loot.Length > 0 ? $"{loot[0].item} ({loot[0].chance:P0})" : "brak";
            Debug.Log($"[Wyk3] NestedTypesEnemy: stan {state}, łupów {loot.Length}, pierwszy: {first}");
            // Klasa statyczna: wywołanie przez nazwę klasy, bez tworzenia obiektu.
            Debug.Log($"[Wyk3] ArenaMath.GridToWorld(2, 3) = {ArenaMath.GridToWorld(2, 3)}");
        }
    }
}
