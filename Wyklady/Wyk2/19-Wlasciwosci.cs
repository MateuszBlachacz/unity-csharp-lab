// Wykład 2, slajd: Właściwości
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class Health : MonoBehaviour
{
    [SerializeField] private int max = 100;
    private int current;

    public int Max => max;               // tylko odczyt
    public bool IsDead => current <= 0;  // wartość wyliczana

    public int Current                   // z walidacją
    {
        get => current;
        set => current = Mathf.Clamp(value, 0, max);
    }

    public int Kills { get; private set; }   // auto-właściwość

    void Awake() => current = max;
}
