// Wykład 1, slajd: Struktura pliku z kodem C#
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

using System;          // przestrzenie nazw
using UnityEngine;

namespace Arena        // własna przestrzeń nazw
{
    /// <summary>Licznik punktów.</summary>
    public class ScoreCounter : MonoBehaviour
    {
        private int score;           // pole

        public void Add(int points) // metoda
        {
            score += points;         // instrukcja
            /* komentarz
               wielowierszowy */
        }
    }
}
