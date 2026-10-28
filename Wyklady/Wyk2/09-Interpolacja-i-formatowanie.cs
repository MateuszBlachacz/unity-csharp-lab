// Wykład 2, slajd: Interpolacja i formatowanie
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int score = 1250; float time = 83.456f; int lives = 3;

string hud = $"Wynik: {score}  Życia: {lives}";
string t1 = $"Czas: {time:F1} s";          // "Czas: 83,5 s" (pl)
string t2 = $"{score:N0}";                 // "1 250"
string t3 = $"{lives:00}";                 // "03"
string t4 = $"[{score,8}]";                // wyrównanie do 8 znaków

int m = (int)time / 60, s = (int)time % 60;
string clock = $"{m:00}:{s:00}";           // "01:23"

string old = string.Format("Wynik: {0}", score);  // starszy zapis
string inv = time.ToString("F2",
    System.Globalization.CultureInfo.InvariantCulture); // "83.46"
