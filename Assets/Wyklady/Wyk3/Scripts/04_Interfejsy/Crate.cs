// Wyk3, slajd „Jeden interfejs, różne klasy”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Skrzynia nie ma zdrowia, ale też spełnia kontrakt IDamageable: pęka od jednego trafienia.
    public class Crate : MonoBehaviour, IDamageable
    {
        private bool broken;

        public bool IsAlive => !broken;

        public void TakeDamage(int amount)
        {
            if (broken)
            {
                return;
            }

            broken = true;
            Debug.Log($"[Wyk3] {name} rozbita");
            Destroy(gameObject);
        }
    }
}
