using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 90f;
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float pulseAmount = 0.2f;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {
        // Mnożenie przez Time.deltaTime daje stopnie na sekundę, a nie na klatkę.
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.World);
        transform.localScale = baseScale * (1f + pulseAmount * Mathf.Sin(Time.time * pulseSpeed));
    }
}
