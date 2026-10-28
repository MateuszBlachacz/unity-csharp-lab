using UnityEngine;
using UnityEngine.InputSystem;

// Ruch gracza z L2: WASD i strzałki przez Input System, granice areny, sprint z wytrzymałością.
public class PlayerMovement : MonoBehaviour
{
    private const float MaxStamina = 100f;

    [SerializeField] private float speed = 5f;
    // Zadanie 11 ★: jednostki na sekundę do kwadratu.
    [SerializeField] private float acceleration = 20f;
    // x: połowa szerokości w osi X, y: połowa głębokości w osi Z (podłoga 18 × 10 minus promień gracza 0,5)
    [SerializeField] private Vector2 arenaHalfSize = new Vector2(8.5f, 4.5f);

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
    private Vector3 velocity;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        // Zadanie 9 ★: kompozyt WASD w InputSystem_Actions nie ma ustawionego trybu, więc działa
        // w domyślnym DigitalNormalized i klawiatura po skosie daje już długość 1. W trybie Digital
        // byłoby to ok. 1,41. ClampMagnitude skraca tylko dłuższe wektory; normalized wydłużyłby
        // też lekkie wychylenie drążka.
        Vector2 input = Vector2.ClampMagnitude(moveAction.ReadValue<Vector2>(), 1f);

        bool shiftHeld = Keyboard.current.leftShiftKey.isPressed;
        bool isMoving = input.sqrMagnitude > 0f;
        bool sprinting = shiftHeld && isMoving && !exhausted;
        float currentSpeed = sprinting ? speed * sprintMultiplier : speed;

        UpdateStamina(sprinting);

        Vector3 before = transform.position;
        Vector3 dir = new Vector3(input.x, 0f, input.y);   // y wejścia to oś Z świata
        // Zadanie 11 ★: prędkość zbliża się do docelowej najwyżej o acceleration * Time.deltaTime.
        velocity = Vector3.MoveTowards(velocity, dir * currentSpeed, acceleration * Time.deltaTime);
        // Bez Time.deltaTime gracz przesuwałby się o stałą odległość na klatkę: przy speed 0,1
        // to ok. 3 j/s przy 30 FPS i ok. 30 j/s przy 300 FPS.
        transform.position += velocity * Time.deltaTime;
        ClampToArena();

        measuredSpeed = Time.deltaTime > 0f ? Vector3.Distance(before, transform.position) / Time.deltaTime : 0f;
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

    private void ClampToArena()
    {
        // transform.position.x = 0f; się nie kompiluje: właściwość position zwraca kopię struktury Vector3,
        // więc zmiana jednej współrzędnej kopii nic by nie dała. Dlatego kopiujemy, zmieniamy i przypisujemy.
        Vector3 pos = transform.position;
        float clampedX = Mathf.Clamp(pos.x, -arenaHalfSize.x, arenaHalfSize.x);
        float clampedZ = Mathf.Clamp(pos.z, -arenaHalfSize.y, arenaHalfSize.y);

        // Przy krawędzi zerujemy prędkość w tej osi, inaczej po zmianie kierunku gracz
        // najpierw wytracałby prędkość skierowaną w krawędź.
        if (clampedX != pos.x)
        {
            velocity.x = 0f;
        }
        if (clampedZ != pos.z)
        {
            velocity.z = 0f;
        }

        pos.x = clampedX;
        pos.z = clampedZ;
        transform.position = pos;
    }

    // Zadanie 10 ★: dozwolony obszar widoczny w widoku Scene po zaznaczeniu gracza.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 size = new Vector3(arenaHalfSize.x * 2f, 0.1f, arenaHalfSize.y * 2f);
        Gizmos.DrawWireCube(new Vector3(0f, 0.05f, 0f), size);
    }
}
