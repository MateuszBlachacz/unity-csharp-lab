// Wykład 2, slajd: Split: z tekstu do danych
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

string line = "Ala;1250;83.5";
string[] parts = line.Split(';');

string player = parts[0].Trim();
int score = int.Parse(parts[1]);
float time = float.Parse(parts[2],
    System.Globalization.CultureInfo.InvariantCulture);

// poziom: każdy wiersz to rząd areny
string level = "#####\n#..C#\n#.#.#\n#####";
string[] rows = level.Split('\n');
for (int r = 0; r < rows.Length; r++)
    for (int c = 0; c < rows[r].Length; c++)
        if (rows[r][c] == '#') SpawnWall(r, c);
