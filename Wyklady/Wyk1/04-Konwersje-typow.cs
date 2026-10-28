// Wykład 1, slajd: Konwersje typów
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int coins = 7;
float f = coins;          // niejawna: int → float
double d = 3.99;
int i = (int)d;           // jawna: 3 (obcięcie)
int r = Mathf.RoundToInt(3.6f);   // 4

int p = int.Parse("42");  // wyjątek przy złym tekście
bool ok = int.TryParse("4x2", out int n);
                          // ok == false, n == 0
string s = coins.ToString();      // "7"
string t = "Monety: " + coins;    // "Monety: 7"
float avg = 7 / 2;        // 3 – dzielenie całkowite!
float avg2 = 7 / 2f;      // 3.5
