// Wyk4, omówienie zadania projektowego „Zdarzenia w Arenie” (sytuacje 1 i 2)
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private int pointsPerKill = 20;

        private int score;

        private void OnEnable()
        {
            GameEvents.CoinCollected += Add;
            GameEvents.EnemyKilled += OnEnemyKilled;
        }

        private void OnDisable()
        {
            GameEvents.CoinCollected -= Add;
            GameEvents.EnemyKilled -= OnEnemyKilled;
        }

        private void Start()
        {
            GameEvents.RaiseScore(score);
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            Add(pointsPerKill);
        }

        // Publiczna, żeby można ją było podpiąć do UnityEvent<int> w Inspectorze (PressurePlate.scored).
        public void Add(int points)
        {
            score += points;
            Debug.Log($"Wynik +{points} = {score}");
            GameEvents.RaiseScore(score);
        }
    }
}
