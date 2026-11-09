// Wyk3, slajd „Interfejsy w Unity: GetComponent” (obrażenia przy kontakcie)
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class ContactDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float interval = 1f;

        private float nextHitTime;

        private void OnCollisionStay(Collision collision)
        {
            if (Time.time < nextHitTime || !collision.gameObject.TryGetComponent(out DemoPlayer _))
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
                nextHitTime = Time.time + interval;
            }
        }
    }
}
