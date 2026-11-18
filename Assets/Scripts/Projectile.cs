using UnityEngine;

// Zadanie 4 (L6): pocisk leci ze stałą prędkością, rani obiekty z Health i znika
// po trafieniu albo po czasie życia.
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private int damage;
    private Vector3 velocity;

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

    // Wywoływana przez PlayerShooter zaraz po Instantiate, przed pierwszym FixedUpdate.
    public void Init(int damage, Vector3 velocity, float lifetime)
    {
        this.damage = damage;
        this.velocity = velocity;
        Destroy(gameObject, lifetime);
    }

    // Ciało jest kinematyczne, więc przesuwamy je sami w rytmie fizyki.
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pocisk powstaje tuż przy graczu, więc gracza pomijamy.
        if (other.CompareTag("Player"))
        {
            return;
        }

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // Inne triggery (np. monety) pocisk mija.
    }
}
