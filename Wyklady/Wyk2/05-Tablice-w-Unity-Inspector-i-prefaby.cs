// Wykład 2, slajd: Tablice w Unity: Inspector i prefaby
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        foreach (Transform p in spawnPoints)
        {
            int k = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[k], p.position,
                        Quaternion.identity);
        }
    }
}
