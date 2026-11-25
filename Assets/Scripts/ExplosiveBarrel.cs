using System.Collections.Generic;
using UnityEngine;

// Zadanie 7 ★ (L7): beczka przyjmuje obrażenia przez IDamageable, choć nie jest przeciwnikiem
// i nie ma Health. Pierwsze trafienie wywołuje wybuch raniący wszystko w promieniu.
public class ExplosiveBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] private float radius = 2f;
    [SerializeField] private int damage = 25;

    // Chroni przed nieskończoną rekurencją: beczka trafiona wybuchem sąsiedniej beczki
    // odda jej obrażenia, ale ta już wybuchła i drugi raz nie zareaguje.
    private bool exploded;

    public void TakeDamage(int amount)
    {
        if (exploded)
        {
            return;
        }
        exploded = true;
        Debug.Log($"{name}: wybuch!");

        // Obiekt może mieć kilka colliderów, dlatego każdego trafionego ranimy tylko raz.
        var hit = new HashSet<IDamageable>();
        foreach (Collider c in Physics.OverlapSphere(transform.position, radius))
        {
            if (c.gameObject != gameObject && c.TryGetComponent(out IDamageable target) && hit.Add(target))
            {
                target.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
