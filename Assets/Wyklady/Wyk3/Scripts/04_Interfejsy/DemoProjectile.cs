// Wyk3, slajd „Interfejsy w Unity: GetComponent”
using UnityEngine;

namespace Wyklady.Wyk3
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 12f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float lifetime = 3f;

        private GameObject owner;

        public void Launch(Vector3 direction, GameObject shooter)
        {
            owner = shooter;
            GetComponent<Rigidbody>().linearVelocity = direction.normalized * speed;
            Destroy(gameObject, lifetime);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Inne triggery (przedmioty do zebrania) i strzelający nie zatrzymują pocisku.
            if (other.isTrigger || (owner != null && other.transform.IsChildOf(owner.transform)))
            {
                return;
            }

            // Szukamy komponentu po interfejsie, nie po konkretnej klasie.
            if (other.TryGetComponent(out IDamageable target) && target.IsAlive)
            {
                target.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
