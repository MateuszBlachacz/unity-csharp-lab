// Wykład 2, slajd: Niezmienność napisów i StringBuilder
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

// źle: każda iteracja tworzy nowy napis
string log = "";
for (int i = 0; i < 1000; i++)
    log += $"Zdarzenie {i}\n";      // 1000 nowych obiektów

// dobrze: jeden bufor
var sb = new System.Text.StringBuilder();
for (int i = 0; i < 1000; i++)
    sb.Append("Zdarzenie ").Append(i).Append('\n');
string result = sb.ToString();

// w Update: nie twórz napisu, gdy wartość się nie zmieniła
if (score != shownScore)
{
    shownScore = score;
    scoreText.text = $"Wynik: {score}";
}
