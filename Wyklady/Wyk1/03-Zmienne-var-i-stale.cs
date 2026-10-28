// Wykład 1, slajd: Zmienne, var i stałe
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int score = 0;           // deklaracja + wartość
float speed = 4.5f;
var lives = 3;           // typ wywnioskowany: int
var name = "Gracz 1";    // typ wywnioskowany: string

const int MaxLives = 5;  // stała czasu kompilacji
// pole klasy: readonly float delay;
// ustawiane raz: przy deklaracji lub w konstruktorze

int a;
// Debug.Log(a);         // błąd: zmienna nieprzypisana

score = score + 10;
score += 10;             // to samo krócej
