using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 arenaHalfSize = new Vector2(8.5f, 4.5f);

    [Header("Sprint")]
    [SerializeField] private float sprintMultiplier = 1.8f;
    [SerializeField] private float staminaDrain = 30f;
    [SerializeField] private float staminaRegen = 15f;
    [SerializeField] private float staminaRecoverThreshold = 25f;

    // Podgląd w Inspectorze: wartość liczy skrypt, ręczna zmiana zostanie nadpisana.
    [SerializeField] private float stamina = 100f;
    [SerializeField] private bool exhausted;

    private const float MaxStamina = 100f;

    private InputAction moveAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        bool isMoving = input != Vector2.zero;

        bool wantsSprint = Keyboard.current != null
            && Keyboard.current.leftShiftKey.isPressed
            && isMoving
            && stamina > 0f
            && !exhausted;

        UpdateStamina(wantsSprint);

        float currentSpeed = wantsSprint ? speed * sprintMultiplier : speed;
        // y wejścia to oś Z świata.
        Vector3 dir = new Vector3(input.x, 0f, input.y);

        // Pozycja to struktura: kopiujemy, zmieniamy i przypisujemy z powrotem.
        Vector3 pos = transform.position + dir * currentSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -arenaHalfSize.x, arenaHalfSize.x);
        pos.z = Mathf.Clamp(pos.z, -arenaHalfSize.y, arenaHalfSize.y);
        transform.position = pos;
    }

    private void UpdateStamina(bool sprinting)
    {
        float change = sprinting ? -staminaDrain : staminaRegen;
        stamina = Mathf.Clamp(stamina + change * Time.deltaTime, 0f, MaxStamina);

        if (stamina <= 0f)
        {
            exhausted = true;
        }
        else if (exhausted && stamina > staminaRecoverThreshold)
        {
            exhausted = false;
        }
    }
}
