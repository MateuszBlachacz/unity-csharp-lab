using UnityEngine;

public class CoinRing : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int count = 8;
    [SerializeField] private float radius = 4f;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            // Monety rozłożone równo co 2π / count radianów.
            float angle = i * Mathf.PI * 2f / count;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            pos.y = 0.8f;

            // Czwarty argument to rodzic, więc monety lądują pod tym obiektem.
            Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, transform);
        }
    }
}
