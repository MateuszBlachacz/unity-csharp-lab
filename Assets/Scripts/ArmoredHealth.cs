using UnityEngine;

// Zadanie 6 ★ (L7): zdrowie z pancerzem. Dziedziczy wszystko po Health,
// zmienia tylko sposób przyjmowania obrażeń.
public class ArmoredHealth : Health
{
    [SerializeField] private int armor = 5;

    public int Armor => armor;

    // Każde trafienie słabsze o pancerz, ale zawsze co najmniej 1. Wartości <= 0 przekazujemy
    // bez zmian, żeby Health dalej je pomijało (bez tego byłyby zamienione na 1).
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount > 0 ? Mathf.Max(amount - armor, 1) : amount);
    }
}
