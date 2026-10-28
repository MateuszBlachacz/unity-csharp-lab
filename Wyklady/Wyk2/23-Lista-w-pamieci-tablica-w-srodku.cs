// Wykład 2, slajd: Lista w pamięci: tablica w środku
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

var l = new List<int>();   // Count 0, Capacity 0
l.Add(1);                  // Count 1, Capacity 4
l.Add(2); l.Add(3); l.Add(4);   // Count 4, Capacity 4
l.Add(5);                  // Count 5, Capacity 8 (!)

 zmienna     obiekt List<int>        wewnętrzna tablica
+-----+     +----------------+     +---+---+---+---+---+---+---+---+
|  l  |---->| _items --------+---->| 1 | 2 | 3 | 4 | 5 | 0 | 0 | 0 |
+-----+     | _size = 5      |     +---+---+---+---+---+---+---+---+
            +----------------+       Count = 5         wolne

// brak miejsca: nowa tablica 2x większa + kopiowanie
var big = new List<int>(1000);   // od razu Capacity 1000
