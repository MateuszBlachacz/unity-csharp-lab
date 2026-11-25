using System.Reflection;
using UnityEngine;

// Gotowy skrypt pomocniczy do animacji przeciwników z folderu Enemy (nie jest częścią zadań L7).
// - parametr Speed bierze z prędkości Rigidbody (w poziomie), więc chodzenie animuje się samo,
// - animację trafienia (Hit) włącza sam, gdy na obiekcie spadnie Health.Current,
// - animację ataku włączasz ze swojego skryptu: GetComponent<EnemyAnimator>().PlayAttack();
// Skrypt nie odwołuje się bezpośrednio do żadnej klasy z projektu, dlatego folder Enemy
// można skopiować do dowolnego projektu i nic się nie zepsuje.
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int SpeedId = Animator.StringToHash("Speed");
    private static readonly int AttackId = Animator.StringToHash("Attack");
    private static readonly int HitId = Animator.StringToHash("Hit");

    [Tooltip("Tryb pokazowy: zwierzak sam przełącza bieg, atak i trafienie (do sceny EnemyShowcase).")]
    [SerializeField] private bool showcase;

    private Animator animator;
    private Rigidbody rb;

    // Komponent Health szukamy po nazwie, żeby skrypt kompilował się także bez niego.
    private Component health;
    private PropertyInfo currentProperty;
    private int lastHealth = int.MinValue;

    private float showcaseOffset;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        showcaseOffset = transform.GetSiblingIndex() * 0.9f;
    }

    private void Start()
    {
        // W Start, bo Health ustawia Current w swoim Awake.
        foreach (Component c in GetComponents<Component>())
        {
            if (c == null) continue;
            PropertyInfo p = c.GetType().GetProperty("Current", BindingFlags.Public | BindingFlags.Instance);
            if (p != null && p.PropertyType == typeof(int))
            {
                health = c;
                currentProperty = p;
                lastHealth = (int)p.GetValue(c);
                break;
            }
        }
    }

    private void Update()
    {
        if (showcase)
        {
            UpdateShowcase();
            return;
        }

        float speed = 0f;
        if (rb != null && !rb.isKinematic)
        {
            Vector3 v = rb.linearVelocity;
            v.y = 0f;
            speed = v.magnitude;
        }
        animator.SetFloat(SpeedId, speed);

        if (health != null)
        {
            int current = (int)currentProperty.GetValue(health);
            if (current < lastHealth) PlayHit();
            lastHealth = current;
        }
    }

    public void PlayAttack() => animator.SetTrigger(AttackId);

    public void PlayHit() => animator.SetTrigger(HitId);

    // Cykl 8 s: stoi, biegnie, atakuje, dostaje.
    private void UpdateShowcase()
    {
        float prev = (Time.time - Time.deltaTime + showcaseOffset) % 8f;
        float now = (Time.time + showcaseOffset) % 8f;

        animator.SetFloat(SpeedId, now > 2f && now < 4.5f ? 3f : 0f);
        if (Crossed(prev, now, 5f)) PlayAttack();
        if (Crossed(prev, now, 6.8f)) PlayHit();
    }

    private static bool Crossed(float prev, float now, float mark) =>
        prev < now ? prev < mark && now >= mark : prev < mark || now >= mark;
}
