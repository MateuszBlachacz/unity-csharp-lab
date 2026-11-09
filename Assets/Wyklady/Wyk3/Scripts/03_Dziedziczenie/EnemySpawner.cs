// Wyk3, slajd „Polimorfizm w praktyce”
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk3
{
    public class EnemySpawner : MonoBehaviour
    {
        // Jedna tablica typu bazowego mieści prefaby Chaser, Turret i Tank.
        [SerializeField] private Enemy[] prefabs = new Enemy[0];
        [SerializeField] private int waveSize = 3;
        [SerializeField] private float floorHalfSize = 8.5f;
        [SerializeField] private float minDistanceToPlayer = 3f;

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.digit1Key.wasPressedThisFrame)
            {
                SpawnWave(waveSize);
            }

            if (keyboard.kKey.wasPressedThisFrame)
            {
                KillAll();
            }
        }

        public void SpawnWave(int count)
        {
            DemoPlayer player = FindAnyObjectByType<DemoPlayer>();
            for (int i = 0; i < count; i++)
            {
                Enemy prefab = prefabs[Random.Range(0, prefabs.Length)];
                Vector3 pos = RandomPoint(prefab.transform.position.y, player);
                Enemy enemy = Instantiate(prefab, pos, Quaternion.identity);
                enemy.name = prefab.name;
            }

            Debug.Log($"[Wyk3] Fala: {count} przeciwników");
        }

        public void KillAll()
        {
            Enemy[] enemies = FindObjectsByType<Enemy>();
            // Polimorfizm: to samo wywołanie Die(), a Tank wykona swoją wersję z wybuchem.
            foreach (Enemy enemy in enemies)
            {
                enemy.Die();
            }

            Debug.Log($"[Wyk3] KillAll: {enemies.Length} przeciwników");
        }

        private Vector3 RandomPoint(float y, DemoPlayer player)
        {
            Vector3 pos = Vector3.zero;
            for (int attempt = 0; attempt < 10; attempt++)
            {
                pos = new Vector3(Random.Range(-floorHalfSize, floorHalfSize), y, Random.Range(-floorHalfSize, floorHalfSize));
                if (player == null || Vector3.Distance(pos, player.transform.position) >= minDistanceToPlayer)
                {
                    break;
                }
            }

            return pos;
        }
    }
}
