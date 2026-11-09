// Wyk3, slajd „Interfejs: kontrakt” (zdrowie implementujące IDamageable)
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class DemoHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int max = 100;

        private int current;
        private float invulnerableUntil;

        public int Current => current;
        public int Max => max;
        public bool IsAlive => current > 0;
        public bool IsInvulnerable => Time.time < invulnerableUntil;

        private void Awake()
        {
            current = max;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive || amount <= 0 || IsInvulnerable)
            {
                return;
            }

            current = Mathf.Max(current - amount, 0);
            Debug.Log($"[Wyk3] {name}: -{amount}, zdrowie {current}/{max}");
            if (current == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (!IsAlive || amount <= 0)
            {
                return;
            }

            current = Mathf.Min(current + amount, max);
            Debug.Log($"[Wyk3] {name}: +{amount}, zdrowie {current}/{max}");
        }

        // Tarcza: zdrowie samo decyduje, czy przyjąć obrażenia (pole pozostaje prywatne).
        public void SetInvulnerable(float seconds)
        {
            invulnerableUntil = Mathf.Max(invulnerableUntil, Time.time + seconds);
        }

        private void Die()
        {
            if (TryGetComponent(out Enemy enemy))
            {
                enemy.Die();
            }
            else if (TryGetComponent(out DemoPlayer _))
            {
                Debug.Log("[Wyk3] Gracz pokonany");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
