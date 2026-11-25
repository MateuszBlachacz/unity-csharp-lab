using UnityEngine;

// Zadanie 4 (L6): pocisk leci ze stałą prędkością, rani trafiony obiekt i znika
// po trafieniu albo po czasie życia.
// L7: rani wszystko, co implementuje IDamageable; pociski gracza pomijają gracza,
// a pociski przeciwników pomijają przeciwników.
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private int damage;
    private Vector3 velocity;
    private bool fromPlayer;

    // Zadanie 6 ★: liczba istniejących pocisków. Pole statyczne należy do klasy,
    // a nie do obiektu, więc wszystkie pociski zmieniają ten sam licznik.
    public static int AliveCount { get; private set; }

    // Przy wyłączonym Domain Reload pola statyczne zachowują wartość między uruchomieniami
    // gry w edytorze, dlatego zerujemy licznik przed wczytaniem sceny.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetCount()
    {
        AliveCount = 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        AliveCount++;
    }

    private void OnDestroy()
    {
        AliveCount--;
    }

    // Wywoływana przez strzelającego (PlayerShooter, Shooter) zaraz po Instantiate,
    // przed pierwszym FixedUpdate.
    public void Init(int damage, Vector3 velocity, float lifetime, bool fromPlayer)
    {
        this.damage = damage;
        this.velocity = velocity;
        this.fromPlayer = fromPlayer;
        Destroy(gameObject, lifetime);
    }

    // Ciało jest kinematyczne, więc przesuwamy je sami w rytmie fizyki.
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pocisk gracza powstaje tuż przy graczu i nie może go ranić; pocisk przeciwnika
        // nie rani przeciwników (także tego, który go wystrzelił).
        if (fromPlayer && other.CompareTag("Player"))
        {
            return;
        }
        if (!fromPlayer && other.GetComponentInParent<Enemy>() != null)
        {
            return;
        }

        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // Inne triggery (np. monety) pocisk mija.
    }
}
