// Wykład 2, slajd: Przeciążanie metod
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public void TakeDamage(int amount)
{
    hp -= amount;
}

public void TakeDamage(int amount, Vector3 hitPoint)
{
    TakeDamage(amount);           // wywołanie wersji z 1 parametrem
    SpawnHitEffect(hitPoint);
}

public void TakeDamage(float percent)
{
    TakeDamage(Mathf.RoundToInt(maxHp * percent));
}

TakeDamage(10);                   // wersja (int)
TakeDamage(10, hit.point);        // wersja (int, Vector3)
TakeDamage(0.25f);                // wersja (float)
