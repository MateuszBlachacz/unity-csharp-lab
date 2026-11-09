// Wyk3, slajd „Interfejsy z biblioteki: IComparable”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class ScoreRankingDemo : MonoBehaviour
    {
        private void Start()
        {
            var ranking = new List<ScoreEntry>
            {
                new ScoreEntry("Ala", 120),
                new ScoreEntry("Bartek", 340),
                new ScoreEntry("Celina", 90),
                new ScoreEntry("Darek", 210)
            };

            ranking.Sort();     // używa ScoreEntry.CompareTo

            Debug.Log("[Wyk3] Ranking (malejąco):");
            for (int i = 0; i < ranking.Count; i++)
            {
                Debug.Log($"[Wyk3]   {i + 1}. {ranking[i]}");
            }
        }
    }
}
