using UnityEngine;

// Zadanie 2 (L7): wspólna, abstrakcyjna baza przeciwników. Klasy abstrakcyjnej nie da się
// dodać jako komponentu (Add Component jej nie pokazuje), można dodać tylko klasy pochodne.
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    // protected: pola widać w klasach pochodnych (Chaser, Shooter), ale nie w innych skryptach.
    [SerializeField] protected float speed = 2f;
    protected Transform player;
    protected Rigidbody rb;
    // Animacje z folderu Enemy; przeciwnik działa także bez nich.
    protected EnemyAnimator anim;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        TryGetComponent(out anim);
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Act();
        }
    }

    // Każdy przeciwnik sam decyduje, co robi w kroku fizyki.
    protected abstract void Act();

    public virtual string Describe() => $"{name}: przeciwnik, prędkość {speed}";

    // Kierunek do gracza w poziomie (y = 0), długość 1; zero, gdy stoimy na graczu.
    protected Vector3 FlatDirectionToPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        return dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector3.zero;
    }

    // Ruch tylko w poziomie; prędkość pionową zostawiamy grawitacji.
    protected void MoveHorizontally(Vector3 dir, float moveSpeed)
    {
        rb.linearVelocity = new Vector3(dir.x * moveSpeed, rb.linearVelocity.y, dir.z * moveSpeed);
    }

    protected void FaceDirection(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
    }
}
