// Wykład 1, slajd: Operatory arytmetyczne i inkrementacja
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int a = 9, b = 2;
int q = a / b;          // 4   dzielenie całkowite
int m = a % b;          // 1   reszta z dzielenia
float x = a / b;        // 4   (!) najpierw int / int
float y = (float)a / b; // 4.5

int i = 5;
int j = i++;            // j = 5, i = 6  (post-)
int k = ++i;            // i = 7, k = 7  (pre-)

int t1 = 2 + 3 * 4;     // 14
int t2 = (2 + 3) * 4;   // 20
bool even = i % 2 == 0; // parzystość
