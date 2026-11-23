// Wyk4, slajd „ScriptableObject: dane jako asset” (pocisk z prefabu wskazanego w WeaponData)
using UnityEngine;

namespace Wyklady.Wyk4
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 15f;
        [SerializeField] private float lifetime = 2f;

        private Rigidbody rb;

        public int Damage { get; set; }
        public GameObject Owner { get; set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + transform.forward * (speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
            {
                return; // mija przedmioty i płytę
            }
            if (Owner != null && other.transform.IsChildOf(Owner.transform))
            {
                return; // nie trafia strzelającego
            }

            // Collider dziecka (np. Head) należy do ciała z Rigidbody, a Health jest na ciele.
            Component body = other.attachedRigidbody != null ? other.attachedRigidbody : other;
            if (body.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}
