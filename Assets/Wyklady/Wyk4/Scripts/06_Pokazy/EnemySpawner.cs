// Wyk4, slajdy „Kolejka i stos” i pokazowy „Rejestr przeciwników i ekwipunek”
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-5)]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy prefab;
        [SerializeField] private Transform player;
        [SerializeField] private float halfSize = 8.5f;
        [SerializeField] private float minDistanceToPlayer = 4f;

        private readonly Queue<int> waves = new();
        private bool gameOver;

        private void Awake()
        {
            waves.Enqueue(3);
            waves.Enqueue(5);
            waves.Enqueue(8);
        }

        private void OnEnable()
        {
            GameEvents.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameEvents.GameOver -= OnGameOver;
        }

        private void Start()
        {
            Debug.Log("=== Wyk4, część 6: pokazy (rejestr przeciwników, ekwipunek, pasek zdrowia i Game Over) ===");
            Debug.Log($"Fale w kolejce: {waves.Count}. Klawisz N wypuszcza następną.");
        }

        private void OnGameOver()
        {
            gameOver = true;
        }

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (gameOver || kb == null || !kb.nKey.wasPressedThisFrame)
            {
                return;
            }

            if (waves.Count > 0)
            {
                int size = waves.Dequeue();
                SpawnWave(size);
                Debug.Log($"Fala: {size} przeciwników, zostało fal: {waves.Count}");
            }
            else if (!waves.TryDequeue(out _))
            {
                Debug.Log("Koniec fal (TryDequeue zwrócił false)");
            }
        }

        // Uzupełnienie do slajdu: przeciwnicy w losowych punktach podłogi, z dala od gracza.
        private void SpawnWave(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Vector3 pos = RandomPoint();
                Enemy e = Instantiate(prefab, pos, Quaternion.identity);
                if (Random.value < 0.5f)
                {
                    e.Configure("Biegacz", 3.5f);
                }
                e.name = $"{prefab.name}_{e.TypeName}_{Enemy.All.Count}";
            }
        }

        private Vector3 RandomPoint()
        {
            Vector3 pos = Vector3.zero;
            for (int attempt = 0; attempt < 20; attempt++)
            {
                pos = new Vector3(Random.Range(-halfSize, halfSize), 1f, Random.Range(-halfSize, halfSize));
                if (Vector3.Distance(pos, player.position) >= minDistanceToPlayer)
                {
                    break;
                }
            }
            return pos;
        }
    }
}
