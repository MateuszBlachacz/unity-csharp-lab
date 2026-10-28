// Wykład 1, slajd: Update, FixedUpdate i Time.deltaTime
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody body;

    void Awake() => body = GetComponent<Rigidbody>();

    void Update()   // ruch bez fizyki
    {
        transform.position +=
            Vector3.right * speed * Time.deltaTime;
    }

    void FixedUpdate()   // ruch z fizyką (Unity 6)
    {
        Vector3 v = body.linearVelocity;
        body.linearVelocity = new Vector3(0f, v.y, speed);
    }
}
