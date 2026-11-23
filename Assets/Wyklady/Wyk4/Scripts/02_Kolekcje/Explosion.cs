// Wyk4, slajd „Zbiór: HashSet<T>” (każdy cel trafiony tylko raz)
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private float radius = 5f;
        [SerializeField] private int damage = 10;

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb != null && kb.eKey.wasPressedThisFrame)
            {
                Explode();
            }
        }

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(center.position, radius);
            // Przeciwnik ma dwa collidery (ciało i Head), ale zbiór przyjmie go tylko raz.
            HashSet<IDamageable> damaged = new HashSet<IDamageable>();
            foreach (Collider hit in hits)
            {
                if (hit.transform.IsChildOf(center))
                {
                    continue; // wybuch nie rani gracza
                }

                IDamageable target = hit.GetComponentInParent<IDamageable>();
                if (target != null && damaged.Add(target))
                {
                    target.TakeDamage(damage);
                }
            }
            Debug.Log($"Wybuch: trafiono {hits.Length} colliderów, obrażenia zadano {damaged.Count} celom");
        }
    }
}
