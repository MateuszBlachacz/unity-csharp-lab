using UnityEngine;
using UnityEngine.InputSystem;

// Ruch gracza: WASD i strzałki przez Input System, sprint z wytrzymałością (L2),
// od L4 przez prędkość Rigidbody, więc gracza zatrzymują ściany areny.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private const float MaxStamina = 100f;

    [SerializeField] private float speed = 5f;
    // Zadanie 11 ★ z L2: jednostki na sekundę do kwadratu.
    [SerializeField] private float acceleration = 20f;

    [Header("Sprint")]
    [SerializeField] private float sprintMultiplier = 1.8f;
    [SerializeField] private float staminaDrain = 30f;
    [SerializeField] private float staminaRegen = 15f;
    [SerializeField] private float recoverThreshold = 25f;

    // Pola podglądu: tylko do odczytu w Inspectorze, wartości ustawia skrypt.
    [Header("Podgląd")]
    [SerializeField] private float measuredSpeed;
    [SerializeField] private float stamina = MaxStamina;
    [SerializeField] private bool exhausted;

    private InputAction moveAction;
    private Rigidbody rb;
    // Prędkość docelowa w poziomie liczona w Update z wejścia, używana w FixedUpdate.
    private Vector3 targetVelocity;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Zadanie 9 ★ z L2: kompozyt WASD działa w trybie DigitalNormalized, więc po skosie
        // daje już długość 1. ClampMagnitude skraca tylko dłuższe wektory (np. w trybie Digital).
        Vector2 input = Vector2.ClampMagnitude(moveAction.ReadValue<Vector2>(), 1f);

        bool shiftHeld = Keyboard.current.leftShiftKey.isPressed;
        bool isMoving = input.sqrMagnitude > 0f;
        bool sprinting = shiftHeld && isMoving && !exhausted;
        float currentSpeed = sprinting ? speed * sprintMultiplier : speed;

        UpdateStamina(sprinting);

        // y wejścia to oś Z świata
        targetVelocity = new Vector3(input.x, 0f, input.y) * currentSpeed;

        // Podgląd: faktyczna prędkość pozioma ciała (przy ścianie spada do zera).
        Vector3 v = rb.linearVelocity;
        measuredSpeed = new Vector2(v.x, v.z).magnitude;
    }

    // FixedUpdate działa w rytmie fizyki, więc tu zmieniamy prędkość ciała.
    private void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;
        // Bieżąca prędkość pozioma pochodzi z ciała, więc po uderzeniu w ścianę jest już
        // wyhamowana i rozpędzanie zaczyna się od niej, a nie od zapamiętanej wartości.
        Vector3 horizontal = new Vector3(v.x, 0f, v.z);
        // Zadanie 11 ★ z L2: zbliżanie się do prędkości docelowej najwyżej o acceleration * dt.
        horizontal = Vector3.MoveTowards(horizontal, targetVelocity, acceleration * Time.fixedDeltaTime);
        // Prędkość to już jednostki na sekundę, więc nie mnożymy przez Time.deltaTime.
        // Składową y zostawiamy, żeby działała grawitacja.
        rb.linearVelocity = new Vector3(horizontal.x, v.y, horizontal.z);
    }

    private void UpdateStamina(bool sprinting)
    {
        stamina += sprinting ? -staminaDrain * Time.deltaTime : staminaRegen * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, MaxStamina);

        if (stamina <= 0f)
        {
            exhausted = true;
        }
        else if (exhausted && stamina > recoverThreshold)
        {
            exhausted = false;
        }
    }
}
