// Wyk3, slajd „Hermetyzacja w praktyce” (wersja z hermetyzacją)
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Pole jest prywatne, a zmienić je można tylko przez TakeDamage i Heal.
    // Dzięki temu klasa sama pilnuje niezmienników: zdrowie zawsze mieści się
    // w zakresie 0..max, a dojście do zera zawsze kończy się wywołaniem Die().
    public class HealthEncapsulated : MonoBehaviour
    {
        [SerializeField] private int max = 100;

        private int current;

        public int Current => current;
        public int Max => max;

        private void Awake()
        {
            current = max;
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || current == 0)
            {
                return;
            }

            current = Mathf.Clamp(current - amount, 0, max);
            if (current == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || current == 0)
            {
                return;
            }

            current = Mathf.Clamp(current + amount, 0, max);
        }

        // Uzupełnienie do slajdu: w przykładzie wystarczy komunikat.
        private void Die()
        {
            Debug.Log($"[Wyk3] {name}: zdrowie spadło do zera, Die()");
        }
    }
}
