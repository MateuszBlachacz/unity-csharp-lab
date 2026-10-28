// Wykład 2, slajd: Tablica w pamięci
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int[] scores = { 120, 80, 95, 0, 60 };

 zmienna              obiekt tablicy na stercie
+--------+     +--------+-----+-----+-----+-----+-----+
| scores |---->| Length | 120 |  80 |  95 |   0 |  60 |
+--------+     |   5    | [0] | [1] | [2] | [3] | [4] |
               +--------+-----+-----+-----+-----+-----+
                        ^ +0  ^ +4  ^ +8  ^ +12 ^ +16 B

// adres scores[i] = adres scores[0] + i * 4 B (int)

int[] copy = scores;   // druga strzałka do tego samego bloku
