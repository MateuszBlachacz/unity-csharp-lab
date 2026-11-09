// Wyk3, slajd „Zadanie projektowe: przedmioty do zebrania” (uproszczony gracz do pokazów)
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk3
{
    [RequireComponent(typeof(Rigidbody), typeof(DemoHealth))]
    public class DemoPlayer : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float arenaHalfSize = 9.5f;
        [SerializeField] private DemoProjectile projectilePrefab;
        [SerializeField] private float shotCooldown = 0.25f;

        private Rigidbody body;
        private DemoHealth health;
        private int score;
        private Vector2 input;
        private Vector3 facing = Vector3.forward;
        private float nextShotTime;

        public DemoHealth Health => health;
        public int Score => score;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            health = GetComponent<DemoHealth>();
        }

        private void Update()
        {
            input = Vector2.zero;
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || !DemoGame.IsRunning)
            {
                return;
            }

            if (keyboard.wKey.isPressed) input.y += 1f;
            if (keyboard.sKey.isPressed) input.y -= 1f;
            if (keyboard.dKey.isPressed) input.x += 1f;
            if (keyboard.aKey.isPressed) input.x -= 1f;
            input = Vector2.ClampMagnitude(input, 1f);

            if (keyboard.spaceKey.wasPressedThisFrame && Time.time >= nextShotTime)
            {
                Shoot();
            }
        }

        private void FixedUpdate()
        {
            Vector3 dir = new Vector3(input.x, 0f, input.y);
            if (dir != Vector3.zero)
            {
                facing = dir.normalized;
            }

            // Prędkość to struktura: budujemy nowy wektor, zachowując składową y (grawitację).
            body.linearVelocity = new Vector3(dir.x * speed, body.linearVelocity.y, dir.z * speed);

            Vector3 pos = body.position;
            Vector3 clamped = new Vector3(
                Mathf.Clamp(pos.x, -arenaHalfSize, arenaHalfSize),
                pos.y,
                Mathf.Clamp(pos.z, -arenaHalfSize, arenaHalfSize));
            if (clamped != pos)
            {
                body.position = clamped;
            }
        }

        private void Shoot()
        {
            nextShotTime = Time.time + shotCooldown;
            Vector3 spawn = transform.position + facing * 0.8f;
            DemoProjectile shot = Instantiate(projectilePrefab, spawn, Quaternion.identity);
            shot.Launch(facing, gameObject);
        }

        public void AddScore(int points)
        {
            score += points;
            Debug.Log($"[Wyk3] Punkty: +{points}, razem {score}");
        }

        public void SetInvulnerable(float seconds)
        {
            health.SetInvulnerable(seconds);
            Debug.Log($"[Wyk3] Tarcza na {seconds} s");
        }
    }
}
