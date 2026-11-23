// Wyk4, slajd „Słownik w praktyce” (licznik zabójstw po typie)
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    public class KillStats : MonoBehaviour
    {
        private readonly Dictionary<string, int> killsByType = new();

        private void OnEnable()
        {
            GameEvents.EnemyKilled += OnEnemyKilled;
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= OnEnemyKilled;
        }

        private void OnEnemyKilled(Enemy e)
        {
            killsByType.TryGetValue(e.TypeName, out int n);
            killsByType[e.TypeName] = n + 1;
        }

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb != null && kb.tKey.wasPressedThisFrame)
            {
                PrintStats();
            }
        }

        private void PrintStats()
        {
            if (killsByType.Count == 0)
            {
                Debug.Log("Statystyki: brak zabójstw");
                return;
            }

            int total = 0;
            foreach (var (type, count) in killsByType)
            {
                Debug.Log($"Zabici – {type}: {count}");
                total += count;
            }
            Debug.Log($"Zabici razem: {total}");
        }
    }
}
