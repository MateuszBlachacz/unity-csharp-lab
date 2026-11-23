// Wyk4, slajd pokazowy „Pasek zdrowia i Game Over” (gracz sceny pokazowej)
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    [RequireComponent(typeof(Rigidbody), typeof(Health))]
    public class DemoPlayer : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private Weapon weapon;
        [SerializeField] private int testDamage = 20;

        private Rigidbody rb;
        private Health health;
        private Vector3 move;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
        }

        private void OnDisable()
        {
            move = Vector3.zero;
            if (rb != null)
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb == null)
            {
                return;
            }

            Vector2 input = Vector2.zero;
            if (kb.wKey.isPressed) input.y += 1f;
            if (kb.sKey.isPressed) input.y -= 1f;
            if (kb.dKey.isPressed) input.x += 1f;
            if (kb.aKey.isPressed) input.x -= 1f;
            input = Vector2.ClampMagnitude(input, 1f);
            move = new Vector3(input.x, 0f, input.y);

            if (kb.spaceKey.isPressed)
            {
                weapon.Fire(); // tempo ogranicza Weapon (fireRate z WeaponData)
            }

            // Szybki pokaz paska zdrowia i Game Over bez czekania na przeciwników.
            if (kb.hKey.wasPressedThisFrame)
            {
                health.TakeDamage(testDamage);
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
            if (move.sqrMagnitude > 0.01f)
            {
                rb.MoveRotation(Quaternion.LookRotation(move));
            }
        }
    }
}
