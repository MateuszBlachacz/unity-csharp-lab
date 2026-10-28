// Wykład 2, slajd: Tablica jednowymiarowa
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int[] scores = new int[5];          // 5 zer
string[] names = { "Ala", "Ola", "Ela" };
float[] times = new float[] { 12.5f, 9.8f };

scores[0] = 120;                    // pierwszy element
scores[4] = 80;                     // ostatni element
int n = scores.Length;              // 5

Debug.Log(names[1]);                // "Ola"
// scores[5] = 1;   // IndexOutOfRangeException

int[] copy = scores;                // ta sama tablica!
copy[0] = 0;
Debug.Log(scores[0]);               // 0
