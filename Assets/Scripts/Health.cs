using UnityEngine;

// Zadanie 1 (L6): zdrowie obiektu. Bieżącą wartość można z zewnątrz tylko odczytać,
// zmienia się wyłącznie przez TakeDamage i Heal.
// Zadanie 1 (L7): Health implementuje IDamageable, więc pocisk rani go przez interfejs.
public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    // Publiczny odczyt, prywatny zapis: health.Current = 100; w innym skrypcie
    // kończy się błędem kompilatora CS0272.
    public int Current { get; private set; }
    public int Max => maxHealth;
    public bool IsDead => Current == 0;

    private void Awake()
    {
        Current = maxHealth;
    }

    // virtual (L7): klasa pochodna może zmienić sposób przyjmowania obrażeń.
    public virtual void TakeDamage(int amount)
    {
        // Ujemne obrażenia leczyłyby, dlatego je pomijamy; martwego nie ranimy drugi raz.
        if (amount <= 0 || IsDead)
        {
            return;
        }

        Current = Mathf.Max(Current - amount, 0);
        Debug.Log($"{name}: -{amount} zdrowia, zostało {Current}/{maxHealth}");

        if (IsDead)
        {
            Debug.Log($"{name} nie żyje.");
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || IsDead)
        {
            return;
        }

        Current = Mathf.Min(Current + amount, maxHealth);
        Debug.Log($"{name}: +{amount} zdrowia, teraz {Current}/{maxHealth}");
    }
}
