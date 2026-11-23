// Wyk4, slajd „Globalne zdarzenia gry” i rozwiązanie zadania projektowego „Zdarzenia w Arenie”
using System;
using UnityEngine;

namespace Wyklady.Wyk4
{
    public static class GameEvents
    {
        public static event Action<int> ScoreChanged;
        public static event Action GameOver;

        // Z rozwiązania zadania projektowego.
        public static event Action<Enemy> EnemyKilled;
        public static event Action<int> CoinCollected;

        public static void RaiseScore(int score) => ScoreChanged?.Invoke(score);
        public static void RaiseGameOver() => GameOver?.Invoke();
        public static void RaiseEnemyKilled(Enemy enemy) => EnemyKilled?.Invoke(enemy);
        public static void RaiseCoinCollected(int value) => CoinCollected?.Invoke(value);

        // Statyczne zdarzenia przeżywają zmianę sceny, a przy wyłączonym przeładowaniu domeny
        // także wyjście z trybu Play. Czyścimy je przy każdym starcie gry.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetEvents()
        {
            ScoreChanged = null;
            GameOver = null;
            EnemyKilled = null;
            CoinCollected = null;
        }
    }
}
