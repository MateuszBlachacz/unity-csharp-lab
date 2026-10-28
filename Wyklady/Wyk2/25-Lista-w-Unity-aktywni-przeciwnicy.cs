// Wykład 2, slajd: Lista w Unity: aktywni przeciwnicy
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Transform> spawnPoints;
    List<GameObject> alive = new List<GameObject>();

    public int AliveCount => alive.Count;

    public void SpawnWave()
    {
        foreach (Transform p in spawnPoints)
            alive.Add(Instantiate(enemyPrefab, p.position,
                                  Quaternion.identity));
    }
    public void OnEnemyDied(GameObject enemy)
    {
        alive.Remove(enemy);
        Destroy(enemy);
        if (alive.Count == 0) Debug.Log("Fala pokonana!");
    }
}
