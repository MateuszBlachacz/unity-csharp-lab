// Wyk4, slajd „Przykład: osiągnięcia na zdarzeniach”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class Achievements : MonoBehaviour
    {
        private readonly HashSet<string> unlocked = new();
        private int kills;

        private void OnEnable()
        {
            GameEvents.EnemyKilled += OnEnemyKilled;
            GameEvents.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= OnEnemyKilled;
            GameEvents.GameOver -= OnGameOver;
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            kills++;
            if (kills >= 10)
            {
                Unlock("Łowca");
            }
        }

        private void OnGameOver()
        {
            if (kills == 0)
            {
                Unlock("Pacyfista");
            }
        }

        private void Unlock(string id)
        {
            if (unlocked.Add(id)) // Add zwraca false, gdy już było – komunikat tylko raz
            {
                Debug.Log($"Osiągnięcie odblokowane: {id}");
            }
        }
    }
}
