using UnityEngine;

// Zadanie 4 (L7): przeciwnik trzyma dystans od gracza i co jakiś czas strzela.
public class Shooter : Enemy
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float preferredDistance = 5f;
    [SerializeField] private float distanceTolerance = 0.5f;
    [SerializeField] private float shotInterval = 2f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float projectileSpeed = 8f;
    [SerializeField] private float projectileLifetime = 3f;
    [SerializeField] private float muzzleDistance = 0.9f;
    [SerializeField] private float muzzleHeight = 1f;

    private float nextShotTime;

    protected override void Awake()
    {
        // Bez base.Awake() pola rb i player zostałyby puste.
        base.Awake();
        // Pierwszy strzał dopiero po pełnym odstępie, nie od razu po pojawieniu się.
        nextShotTime = Time.time + shotInterval;
    }

    protected override void Act()
    {
        Vector3 dir = FlatDirectionToPlayer();
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;

        // Za daleko – podchodzi, za blisko – cofa się, w strefie tolerancji stoi.
        if (distance > preferredDistance + distanceTolerance)
        {
            MoveHorizontally(dir, speed);
        }
        else if (distance < preferredDistance - distanceTolerance)
        {
            MoveHorizontally(-dir, speed);
        }
        else
        {
            MoveHorizontally(Vector3.zero, 0f);
        }
        FaceDirection(dir);

        if (Time.time >= nextShotTime && dir != Vector3.zero)
        {
            Shoot(dir);
            nextShotTime = Time.time + shotInterval;
        }
    }

    private void Shoot(Vector3 dir)
    {
        if (projectilePrefab == null)
        {
            return;
        }

        Vector3 spawn = transform.position + dir * muzzleDistance;
        spawn.y = muzzleHeight;
        Projectile p = Instantiate(projectilePrefab, spawn, Quaternion.LookRotation(dir));
        // fromPlayer = false: ten pocisk pomija przeciwników, a rani gracza.
        p.Init(damage, dir * projectileSpeed, projectileLifetime, false);
        if (anim != null)
        {
            anim.PlayAttack();
        }
    }

    public override string Describe() =>
        base.Describe() + $", strzela co {shotInterval} s z odległości ok. {preferredDistance}";
}
