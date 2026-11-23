// Wyk4, slajdy „Po co generyki?”, „Metody generyczne”, „Klasy generyczne”,
// „Ograniczenia parametru typu” i „Generyki w API Unity”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-50)]
    public class GenericsDemo : MonoBehaviour
    {
        [SerializeField] private string[] tips = { "Strzelaj spacją", "Zbieraj monety", "Uważaj na fale" };
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform player;

        // Prosty cel obrażeń bez MonoBehaviour, do metody z ograniczeniem IDamageable.
        private class Dummy : IDamageable
        {
            private int hp = 20;

            public bool IsAlive => hp > 0;

            public void TakeDamage(int amount)
            {
                hp = Mathf.Max(hp - amount, 0);
            }
        }

        private void Start()
        {
            WhyGenerics();
            GenericMethods();
            GenericClass();
            Constraints();
            UnityApi();
        }

        private void WhyGenerics()
        {
            Debug.Log("--- Po co generyki? ---");
            Enemy[] found = FindObjectsByType<Enemy>();
            if (found.Length == 0)
            {
                Debug.Log("Brak przeciwników w scenie");
                return;
            }

            // Lista object przyjmie wszystko, ale przy odczycie trzeba rzutować.
            List<object> things = new List<object>();
            things.Add(found[0]);
            things.Add("tekst");
            Enemy first = (Enemy)things[0];
            Debug.Log($"List<object>: {things.Count} elementy, pierwszy po rzutowaniu: {first.name}");
            // Enemy second = (Enemy)things[1]; // skompiluje się, ale w czasie gry: InvalidCastException

            // Lista generyczna: typ sprawdza kompilator, rzutowanie niepotrzebne.
            List<Enemy> enemies = new List<Enemy>(found);
            // enemies.Add("tekst"); // błąd kompilacji CS1503: string to nie Enemy
            Debug.Log($"List<Enemy>: {enemies.Count} przeciwników, pierwszy: {enemies[0].name}");
        }

        private void GenericMethods()
        {
            Debug.Log("--- Metody generyczne ---");
            Debug.Log($"Losowa wskazówka: {GenericUtils.RandomItem(tips)}");
            if (prefabs.Length > 0)
            {
                Debug.Log($"Losowy prefab: {GenericUtils.RandomItem(prefabs).name}");
            }

            int a = 1, b = 2;
            GenericUtils.Swap(ref a, ref b);
            Debug.Log($"Swap(int): a = {a}, b = {b}");

            Vector3 p = Vector3.zero, q = Vector3.one;
            GenericUtils.Swap<Vector3>(ref p, ref q); // typ podany jawnie
            Debug.Log($"Swap<Vector3>: p = {p}, q = {q}");
        }

        private void GenericClass()
        {
            Debug.Log("--- Klasy generyczne ---");
            Registry<Enemy> registry = new Registry<Enemy>();
            foreach (Enemy e in FindObjectsByType<Enemy>())
            {
                registry.Register(e);
            }

            Enemy nearest = registry.Nearest(player.position);
            Debug.Log($"Registry<Enemy>: {registry.Count} wpisy, najbliżej gracza: {(nearest != null ? nearest.name : "brak")}");
            // Registry<int> numbers; // błąd kompilacji CS0315: int nie jest Component
        }

        private void Constraints()
        {
            Debug.Log("--- Ograniczenia parametru typu ---");
            string firstTip = ConstraintExamples.FirstOrNull(new List<string>(tips));
            Debug.Log($"class: FirstOrNull = {firstTip}");
            Debug.Log($"struct: IsDefault(Vector3.zero) = {ConstraintExamples.IsDefault(Vector3.zero)}, IsDefault(5) = {ConstraintExamples.IsDefault(5)}");

            List<int> created = ConstraintExamples.Create<List<int>>();
            Debug.Log($"new(): Create<List<int>>() daje pustą listę, Count = {created.Count}");

            GameObject temp = new GameObject("W4_Temp");
            BoxCollider box1 = ConstraintExamples.GetOrAdd<BoxCollider>(temp);
            BoxCollider box2 = ConstraintExamples.GetOrAdd<BoxCollider>(temp);
            Debug.Log($"Component: drugi GetOrAdd zwraca ten sam komponent = {box1 == box2}");
            Destroy(temp);

            List<Dummy> dummies = new List<Dummy> { new Dummy(), new Dummy() };
            Debug.Log($"IDamageable: DamageAll trafił {ConstraintExamples.DamageAll(dummies, 5)} cele");
            Debug.Log($"IComparable<T>: Max(3, 7) = {ConstraintExamples.Max(3, 7)}, Max(\"ala\", \"ola\") = {ConstraintExamples.Max("ala", "ola")}");

            if (player.TryGetComponent(out Health playerHealth))
            {
                Debug.Log($"MonoBehaviour, IDamageable: {ConstraintExamples.Describe(playerHealth)}");
            }
        }

        private void UnityApi()
        {
            Debug.Log("--- Generyki w API Unity ---");
            Rigidbody rb = player.GetComponent<Rigidbody>();
            Debug.Log($"GetComponent<Rigidbody>: masa gracza {rb.mass}");

            Enemy enemy = FindAnyObjectByType<Enemy>();
            if (enemy != null)
            {
                SphereCollider head = enemy.GetComponentInChildren<SphereCollider>();
                Collider[] colliders = enemy.GetComponentsInChildren<Collider>();
                Debug.Log($"GetComponentInChildren<SphereCollider>: {head.name}, GetComponentsInChildren<Collider>: {colliders.Length}");
                if (enemy.TryGetComponent(out Health h))
                {
                    Debug.Log($"TryGetComponent(out Health): {h.Current}/{h.Max}");
                }
            }

            // Typowane Instantiate: z prefabu typu Enemy dostajemy od razu Enemy, bez GetComponent.
            Enemy spawned = Instantiate(enemyPrefab, new Vector3(0f, -20f, 0f), Quaternion.identity);
            Debug.Log($"Instantiate(Enemy) zwraca {spawned.GetType().Name}: {spawned.name} (zaraz zniszczony)");
            Destroy(spawned.gameObject);

            // Na slajdzie: FindObjectsByType<Enemy>(FindObjectsSortMode.None). W Unity 6000.6 parametr FindObjectsSortMode
            // jest przestarzały (ostrzeżenie CS0618), więc używamy przeciążenia bez niego.
            Enemy[] all = FindObjectsByType<Enemy>();
            Debug.Log($"FindObjectsByType<Enemy>: {all.Length} (z kopią, która zniknie pod koniec klatki)");

            // LevelConfig config = Resources.Load<LevelConfig>("Levels/Config");
            // Wymaga folderu Resources, a on trafia w całości do builda, więc tu go nie tworzymy.
        }
    }
}
