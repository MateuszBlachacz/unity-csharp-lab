// Wykład 2, slajd: Przekazywanie argumentów przez wartość
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

void AddBonus(int points)
{
    points += 100;                 // zmienia tylko kopię
}

void Paint(Renderer r)
{
    r.material.color = Color.red;  // zmienia obiekt
    r = null;                      // zmienia tylko kopię referencji
}

int score = 10;
AddBonus(score);
Debug.Log(score);                  // 10

Paint(myRenderer);
Debug.Log(myRenderer == null);     // false, ale kolor jest czerwony
