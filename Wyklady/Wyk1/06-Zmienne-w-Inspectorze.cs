// Wykład 1, slajd: Zmienne w Inspectorze
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class PlayerMovement : MonoBehaviour
{
    // widoczne w Inspectorze i dla innych klas
    public float speed = 5f;

    // widoczne w Inspectorze, ale prywatne
    [SerializeField] private float sprint = 1.5f;
    [SerializeField, Range(0f, 10f)]
    private float jumpForce = 4f;

    [Header("Debug")]
    [SerializeField] private bool showGizmos;

    private int stepCount;  // niewidoczne
}
