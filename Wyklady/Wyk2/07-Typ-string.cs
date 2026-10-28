// Wykład 2, slajd: Typ string
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

string name = "Arena";
char first = name[0];          // 'A'
int len = name.Length;         // 5
string empty = "";             // pusty napis
string none = null;            // brak napisu

string up = name.ToUpper();    // "ARENA" – nowy napis
Debug.Log(name);               // nadal "Arena"

// name[0] = 'a';   // błąd: string jest niezmienny

string path = "C:\\Gry\\save.txt";   // \\ to jeden \
string raw = @"C:\Gry\save.txt";     // napis dosłowny
string twoLines = "Linia 1\nLinia 2";
