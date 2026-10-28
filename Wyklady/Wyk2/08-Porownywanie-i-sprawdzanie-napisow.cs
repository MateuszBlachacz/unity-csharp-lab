// Wykład 2, slajd: Porównywanie i sprawdzanie napisów
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

string a = "gracz", b = "Gracz";

bool same = a == b;                   // false
bool sameIgnore = string.Equals(a, b,
    StringComparison.OrdinalIgnoreCase);   // true

int order = string.Compare("Ala", "Ola");  // < 0

bool blank = string.IsNullOrWhiteSpace(input);
bool hasAt = email.Contains("@");
bool isCsv = file.EndsWith(".csv");

if (tag == "Player") { }          // działa, ale:
if (other.CompareTag("Player")) { }   // lepiej
