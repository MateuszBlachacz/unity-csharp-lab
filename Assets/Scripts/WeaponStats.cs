using UnityEngine;

// Zadanie 2 (L6): zwykła klasa C# z danymi broni, bez dziedziczenia po MonoBehaviour.
// [System.Serializable] pozwala Unity zapisać ją w scenie i pokazać w Inspectorze
// jako rozwijaną sekcję pola komponentu.
[System.Serializable]
public class WeaponStats
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float shotsPerSecond = 4f;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float projectileLifetime = 2f;

    // Konstruktor bezparametrowy: z niego korzysta Unity przy tworzeniu obiektu,
    // pola dostają wtedy wartości domyślne z inicjalizatorów.
    public WeaponStats()
    {
    }

    public WeaponStats(int damage, float shotsPerSecond, float projectileSpeed, float projectileLifetime)
    {
        this.damage = damage;
        this.shotsPerSecond = shotsPerSecond;
        this.projectileSpeed = projectileSpeed;
        this.projectileLifetime = projectileLifetime;
    }

    public int Damage => damage;
    public float ShotsPerSecond => shotsPerSecond;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileLifetime => projectileLifetime;

    // Odstęp między strzałami w sekundach; Max chroni przed dzieleniem przez zero.
    public float Cooldown => 1f / Mathf.Max(shotsPerSecond, 0.01f);

    public override string ToString() =>
        $"{damage} obr., {shotsPerSecond} strz./s, pocisk {projectileSpeed} j/s przez {projectileLifetime} s";
}
