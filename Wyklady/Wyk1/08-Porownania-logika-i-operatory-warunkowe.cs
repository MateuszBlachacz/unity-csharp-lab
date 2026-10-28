// Wykład 1, slajd: Porównania, logika i operatory warunkowe
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

bool canShoot = ammo > 0 && !reloading; // AND, NOT
bool danger = hp < 20 || enemies > 3;   // OR

// && i || są leniwe: prawa strona tylko w razie potrzeby
if (target != null && target.IsAlive) { /* ... */ }

string label = hp > 0 ? "Żyje" : "Pokonany"; // ?:

string name = saved ?? "Gracz";   // domyślna dla null
int len = saved?.Length ?? 0;     // ?. – gdy nie null

bool same = a == b;     // porównanie
// if (a = b)           // błąd: warunek musi być bool
