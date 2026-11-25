using UnityEngine;
using UnityEngine.InputSystem;

// Zadanie 5 (L7): co kilka sekund tworzy losowego przeciwnika w losowym punkcie.
// Tablica typu Enemy[] przyjmuje prefab z dowolną klasą pochodną (Chaser, Shooter, ...).
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private int maxAlive = 6;

    private float timer;

    private void Update()
    {
        // Przed zamknięciem StartPanel timeScale = 0, więc deltaTime = 0 i licznik stoi.
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawn();
        }

        // timeScale > 0: litera E wpisana w pole nazwy gracza nie wypisuje opisów.
        if (Time.timeScale > 0f && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            DescribeAll();
        }
    }

    private void TrySpawn()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }

        // Na razie liczymy przeciwników wyszukiwaniem na scenie; w L8 zastąpi to lista.
        int alive = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (alive >= maxAlive)
        {
            return;
        }

        Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy enemy = Instantiate(prefab, point.position, point.rotation);
        enemy.name = prefab.name;
        Debug.Log($"Nowy przeciwnik: {enemy.name} w {point.name} ({alive + 1}/{maxAlive})");
    }

    // Polimorfizm: pętla zna tylko typ Enemy, a Describe() wywołuje wersję z klasy pochodnej.
    private void DescribeAll()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Debug.Log($"Przeciwnicy na scenie: {enemies.Length}");
        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy.Describe());
        }
    }
}
