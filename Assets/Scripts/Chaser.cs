using UnityEngine;

// Zadanie 3 (L7): przeciwnik idzie prosto na gracza i rani go przy zetknięciu.
public class Chaser : Enemy
{
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageInterval = 1f;

    private float nextDamageTime;

    protected override void Act()
    {
        Vector3 dir = FlatDirectionToPlayer();
        // Ruch przez prędkość Rigidbody, więc ściany zatrzymują przeciwnika jak każde ciało fizyczne.
        MoveHorizontally(dir, speed);
        FaceDirection(dir);
    }

    // OnCollisionStay działa w każdym kroku fizyki, dopóki ciała się stykają;
    // ogranicznik czasu (jak przy strzelaniu w L6) daje obrażenia najwyżej raz na damageInterval.
    private void OnCollisionStay(Collision collision)
    {
        if (Time.time < nextDamageTime || !collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(contactDamage);
            nextDamageTime = Time.time + damageInterval;
            if (anim != null)
            {
                anim.PlayAttack();
            }
        }
    }

    public override string Describe() =>
        base.Describe() + $", goni gracza ({contactDamage} obr. co {damageInterval} s)";
}
