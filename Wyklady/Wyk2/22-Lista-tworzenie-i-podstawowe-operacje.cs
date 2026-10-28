// Wykład 2, slajd: Lista: tworzenie i podstawowe operacje
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

using System.Collections.Generic;

List<string> names = new List<string>();
var scores = new List<int> { 120, 80, 95 };

names.Add("Ala");                 // na koniec
names.Add("Ola");
names.Insert(0, "Ela");           // Ela, Ala, Ola
names.Remove("Ala");              // Ela, Ola
names.RemoveAt(0);                // Ola

int n = scores.Count;             // 3 (nie Length!)
scores[1] = 100;                  // 120, 100, 95
bool has = scores.Contains(95);   // true
int i = scores.IndexOf(42);       // -1: nie ma
scores.Clear();                   // Count = 0
