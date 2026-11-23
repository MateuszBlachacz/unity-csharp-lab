// Wyk4, slajd pokazowy „Rejestr przeciwników i ekwipunek”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    [RequireComponent(typeof(Health), typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        // Rejestr wszystkich aktywnych przeciwników: zamiast FindObjectsByType co klatkę.
        public static readonly List<Enemy> All = new();

        [SerializeField] private string typeName = "Pełzacz";
        [SerializeField] private float speed = 2f;
        [SerializeField] private int contactDamage = 10;
        [SerializeField] private float contactInterval = 1f;

        private Health health;
        private Rigidbody rb;
        private Transform player;
        private float nextHitTime;

        public string TypeName => typeName;
        public bool IsDead => !health.IsAlive;
        public float Distance => player != null ? Vector3.Distance(transform.position, player.position) : float.MaxValue;

        private void Awake()
        {
            health = GetComponent<Health>();
            rb = GetComponent<Rigidbody>();
            GameObject p = GameObject.FindWithTag("Player");
            player = p != null ? p.transform : null;
            health.Died += OnDied;
        }

        private void OnEnable() => All.Add(this);
        private void OnDisable() => All.Remove(this);

        private void OnDestroy()
        {
            health.Died -= OnDied;
        }

        // Uzupełnienie: spawner nadaje typ i prędkość, żeby statystyki zabójstw miały kilka kluczy.
        public void Configure(string newTypeName, float newSpeed)
        {
            typeName = newTypeName;
            speed = newSpeed;
        }

        private void FixedUpdate()
        {
            if (player == null)
            {
                return;
            }

            Vector3 dir = player.position - rb.position;
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.01f)
            {
                return;
            }

            dir.Normalize();
            rb.linearVelocity = new Vector3(dir.x * speed, rb.linearVelocity.y, dir.z * speed);
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }

        private void OnCollisionStay(Collision collision)
        {
            if (Time.time < nextHitTime || !collision.collider.CompareTag("Player"))
            {
                return;
            }

            if (collision.collider.TryGetComponent(out Health target))
            {
                target.TakeDamage(contactDamage);
                nextHitTime = Time.time + contactInterval;
            }
        }

        private void OnDied()
        {
            GameEvents.RaiseEnemyKilled(this);
            Destroy(gameObject);
        }

        // Lista statyczna przeżywa wyłączone przeładowanie domeny, więc czyścimy ją na starcie gry.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetRegistry()
        {
            All.Clear();
        }
    }
}
