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
    private bool diagonalLogged;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        LogFirstDiagonal(input);

        // Zadanie 6: wektor dłuższy niż 1 dawałby szybszy ruch po przekątnej.
        // Kompozyt WASD w InputSystem_Actions nie ma ustawionego trybu, więc działa
        // domyślny DigitalNormalized i z klawiatury przekątna ma już długość 1.
        // Bez normalizacji (W + D = (1, 1)) długość wynosiłaby √2 ≈ 1,41.
        // ClampMagnitude zabezpiecza ruch niezależnie od ustawień akcji i urządzenia.
        input = Vector2.ClampMagnitude(input, 1f);
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

    private void LogFirstDiagonal(Vector2 input)
    {
        if (diagonalLogged || input.x == 0f || input.y == 0f)
        {
            return;
        }

        Debug.Log($"Długość wektora kierunku po przekątnej: {input.magnitude:F2}");
        diagonalLogged = true;
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
