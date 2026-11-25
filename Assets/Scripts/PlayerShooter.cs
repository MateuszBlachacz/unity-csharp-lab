using UnityEngine;
using UnityEngine.InputSystem;

// Zadanie 3 (L6): gracz obraca się w stronę kursora i strzela lewym przyciskiem myszy.
[RequireComponent(typeof(Rigidbody))]
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private WeaponStats stats = new WeaponStats();
    // Pocisk startuje przed graczem, na wysokości 1 nad podłogą (y = 0).
    [SerializeField] private float muzzleDistance = 0.8f;
    [SerializeField] private float muzzleHeight = 1f;

    private Rigidbody rb;
    private Camera cam;
    private float nextShotTime;
    // Obrót liczony w Update z pozycji kursora, ustawiany ciału w FixedUpdate.
    private Quaternion targetRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        targetRotation = rb.rotation;
    }

    private void Start()
    {
        Debug.Log($"Broń gracza: {stats}");
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;
        // Bez myszy albo przed startem gry (timeScale = 0) nie celujemy i nie strzelamy.
        if (mouse == null || cam == null || Time.timeScale == 0f)
        {
            return;
        }

        Aim(mouse.position.ReadValue());

        // isPressed jest prawdziwe przez cały czas trzymania przycisku, więc ogień jest seryjny,
        // a tempo ogranicza nextShotTime.
        if (mouse.leftButton.isPressed && Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + stats.Cooldown;
        }
    }

    // Gracz ma Rigidbody, więc obracamy ciało w rytmie fizyki. Freeze Rotation blokuje
    // tylko obroty od zderzeń, a MoveRotation działa mimo to.
    private void FixedUpdate()
    {
        rb.MoveRotation(targetRotation);
    }

    // Promień z kamery przez punkt kursora przecinamy z płaszczyzną podłogi (y = 0).
    private void Aim(Vector2 mouseScreen)
    {
        Ray ray = cam.ScreenPointToRay(mouseScreen);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        if (!ground.Raycast(ray, out float distance))
        {
            return;
        }

        Vector3 dir = ray.GetPoint(distance) - transform.position;
        dir.y = 0f;                                   // obrót tylko w poziomie
        if (dir.sqrMagnitude > 0.01f)
        {
            targetRotation = Quaternion.LookRotation(dir);
        }
    }

    private void Shoot()
    {
        // Kierunek z obrotu docelowego, bo ciało dogoni go dopiero w najbliższym FixedUpdate.
        Vector3 forward = targetRotation * Vector3.forward;
        // Środek gracza jest na wysokości 1 (kapsuła 2 j stoi na podłodze), dlatego wysokość
        // pocisku ustawiamy wprost, zamiast dodawać ją do pozycji gracza.
        Vector3 spawn = transform.position + forward * muzzleDistance;
        spawn.y = muzzleHeight;
        // Instantiate z komponentem prefabu zwraca od razu komponent nowego obiektu.
        Projectile p = Instantiate(projectilePrefab, spawn, targetRotation);
        p.Init(stats.Damage, forward * stats.ProjectileSpeed, stats.ProjectileLifetime, true);
    }
}
