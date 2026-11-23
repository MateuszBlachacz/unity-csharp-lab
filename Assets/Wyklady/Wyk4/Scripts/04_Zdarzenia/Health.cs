// Wyk4, slajd „Zdarzenie w C#: event”
using System;
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Wcześniej niż inne skrypty, żeby słuchacze w OnEnable widzieli już ustawione zdrowie.
    [DefaultExecutionOrder(-100)]
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int max = 100;

        private int current;

        // Ogłasza zmianę (bieżące, maksymalne); nikt z zewnątrz nie może go wywołać ani wyzerować.
        public event Action<int, int> Changed;
        public event Action Died;

        public int Current => current;
        public int Max => max;
        public bool IsAlive => current > 0;

        private void Awake()
        {
            current = max;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive || amount <= 0)
            {
                return;
            }

            current = Mathf.Max(current - amount, 0);
            Changed?.Invoke(current, max);
            if (current == 0)
            {
                Died?.Invoke();
            }
        }

        // Uzupełnienie do slajdu: leczenie też ogłasza Changed.
        public void Heal(int amount)
        {
            if (!IsAlive || amount <= 0)
            {
                return;
            }

            current = Mathf.Min(current + amount, max);
            Changed?.Invoke(current, max);
        }
    }
}
