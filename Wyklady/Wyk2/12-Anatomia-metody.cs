// Wykład 2, slajd: Anatomia metody
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

//  modyfikator  typ wyniku  nazwa   parametry
    public       int         Damage(int baseDmg, bool crit)
    {
        int dmg = crit ? baseDmg * 2 : baseDmg;
        return dmg;                  // zwraca wynik i kończy metodę
    }

    private void ResetPlayer()       // void – nic nie zwraca
    {
        transform.position = Vector3.zero;
    }

    private bool IsDead() => hp <= 0;   // treść jako wyrażenie

    // wywołania
    int d = Damage(10, true);        // 20
    ResetPlayer();
    if (IsDead()) GameOver();
