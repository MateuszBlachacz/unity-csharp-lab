// Wyk4, slajdy części 2: kolekcje (od „Lista: List<T>” do „Sprawdź się: kolekcje”)
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-40)]
    public class CollectionsDemo : MonoBehaviour
    {
        private readonly Dictionary<string, int> killsByType = new();

        private void Start()
        {
            Debug.Log("=== Wyk4, część 2: kolekcje ===");
            ListBasics();
            RemoveWhileIterating();
            DictionaryBasics();
            QueueAndStack();
            HashSetBasics();
            DictionaryInPractice();
            SortingLists();
            LinqShort();
            Quiz();
        }

        private static string Show<T>(IEnumerable<T> items) => "[" + string.Join(", ", items) + "]";

        private void ListBasics()
        {
            Debug.Log("--- Lista: List<T> ---");
            List<string> names = new List<string>();
            names.Add("Ala");
            names.Add("Olek");
            names.Add("Ewa");
            Debug.Log($"Add x3: {Show(names)}, Count = {names.Count}"); // Count, nie Length jak w tablicy
            names.Insert(0, "Zuza");
            Debug.Log($"Insert(0, \"Zuza\"): {Show(names)}");
            names.Remove("Olek");
            Debug.Log($"Remove(\"Olek\"): {Show(names)}");
            names.RemoveAt(0);
            Debug.Log($"RemoveAt(0): {Show(names)}");
            Debug.Log($"names[1] = {names[1]}, Contains(\"Ala\") = {names.Contains("Ala")}, IndexOf(\"Ewa\") = {names.IndexOf("Ewa")}");
            names.Clear();
            Debug.Log($"Clear(): Count = {names.Count}");
        }

        private void RemoveWhileIterating()
        {
            Debug.Log("--- Usuwanie elementów podczas przechodzenia ---");
            List<EnemyInfo> enemies = MakeEnemies();

            try
            {
                foreach (EnemyInfo e in enemies)
                {
                    if (e.IsDead)
                    {
                        enemies.Remove(e); // zmiana listy w trakcie foreach
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Debug.Log($"Złapany wyjątek: {ex.GetType().Name}: {ex.Message}");
            }

            // Sposób 1: pętla od końca – usunięcie nie przesuwa elementów jeszcze nieodwiedzonych.
            enemies = MakeEnemies();
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].IsDead)
                {
                    enemies.RemoveAt(i);
                }
            }
            Debug.Log($"Pętla od końca: {Show(enemies)}");

            // Sposób 2: RemoveAll z warunkiem.
            enemies = MakeEnemies();
            int removed = enemies.RemoveAll(e => e.IsDead);
            Debug.Log($"RemoveAll: usunięto {removed}, zostało {Show(enemies)}");

            // Sposób 3: najpierw zebrać, potem usunąć.
            enemies = MakeEnemies();
            List<EnemyInfo> toRemove = new List<EnemyInfo>();
            foreach (EnemyInfo e in enemies)
            {
                if (e.IsDead)
                {
                    toRemove.Add(e);
                }
            }
            foreach (EnemyInfo e in toRemove)
            {
                enemies.Remove(e);
            }
            Debug.Log($"Lista toRemove: {Show(enemies)}");
        }

        private void DictionaryBasics()
        {
            Debug.Log("--- Słownik ---");
            Dictionary<string, int> inventory = new Dictionary<string, int>();
            inventory["coin"] = 10;
            inventory["potion"] = 2;
            inventory.Add("key", 1);
            inventory["coin"] += 5;
            Debug.Log($"coin = {inventory["coin"]}, Count = {inventory.Count}, ContainsKey(\"key\") = {inventory.ContainsKey("key")}");

            if (inventory.TryGetValue("gem", out int gems))
            {
                Debug.Log($"gem = {gems}");
            }
            else
            {
                Debug.Log("TryGetValue(\"gem\"): brak klucza, gems = " + gems);
            }
            // int g = inventory["gem"]; // KeyNotFoundException: indeksator przy odczycie wymaga istniejącego klucza

            try
            {
                inventory.Add("key", 1); // drugi raz ten sam klucz
            }
            catch (ArgumentException ex)
            {
                Debug.Log($"Złapany wyjątek: {ex.GetType().Name}: {ex.Message}");
            }

            inventory.Remove("potion");
            foreach (KeyValuePair<string, int> pair in inventory)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
            }
        }

        private void QueueAndStack()
        {
            Debug.Log("--- Kolejka i stos ---");
            Queue<int> waves = new Queue<int>();
            waves.Enqueue(3);
            waves.Enqueue(5);
            waves.Enqueue(8);
            Debug.Log($"Peek() = {waves.Peek()}, Dequeue() = {waves.Dequeue()}, zostało {waves.Count}");

            Stack<string> screens = new Stack<string>();
            screens.Push("Menu");
            screens.Push("Opcje");
            screens.Push("Dźwięk");
            Debug.Log($"Peek() = {screens.Peek()}, Pop() = {screens.Pop()}, teraz na wierzchu {screens.Peek()}");

            Queue<int> emptyQueue = new Queue<int>();
            Stack<string> emptyStack = new Stack<string>();
            // Dequeue/Pop na pustej kolekcji rzuca InvalidOperationException; wersje Try zwracają false.
            Debug.Log($"TryDequeue na pustej kolejce: {emptyQueue.TryDequeue(out int _)}, TryPop na pustym stosie: {emptyStack.TryPop(out string _)}");
        }

        private void HashSetBasics()
        {
            Debug.Log("--- Zbiór: HashSet<T> ---");
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            Debug.Log(visited.Add(new Vector2Int(2, 3))); // true – nowy element
            Debug.Log(visited.Add(new Vector2Int(2, 3))); // false – już jest
            Debug.Log(visited.Contains(new Vector2Int(2, 3))); // true

            HashSet<string> unlocked = new HashSet<string> { "Poziom 1", "Poziom 2" };
            ShowLocked(unlocked);
        }

        // Uzupełnienie do slajdu: wypisuje poziomy, których nie ma w zbiorze odblokowanych.
        private void ShowLocked(HashSet<string> unlocked)
        {
            string[] all = { "Poziom 1", "Poziom 2", "Poziom 3" };
            foreach (string level in all)
            {
                if (!unlocked.Contains(level))
                {
                    Debug.Log($"Zablokowany: {level}");
                }
            }
        }

        private void DictionaryInPractice()
        {
            Debug.Log("--- Słownik w praktyce ---");
            foreach (EnemyInfo e in MakeEnemies())
            {
                OnEnemyKilled(e);
            }
            PrintStats();
        }

        private void OnEnemyKilled(EnemyInfo e)
        {
            killsByType.TryGetValue(e.TypeName, out int n); // przy braku klucza n = 0
            killsByType[e.TypeName] = n + 1;
        }

        private void PrintStats()
        {
            foreach (KeyValuePair<string, int> pair in killsByType)
            {
                Debug.Log($"{pair.Key}: {pair.Value}");
            }
            foreach (var (type, count) in killsByType) // dekonstrukcja pary
            {
                Debug.Log($"{type} -> {count}");
            }
            Debug.Log($"Razem: {killsByType.Values.Sum()}");
        }

        private void SortingLists()
        {
            Debug.Log("--- Sortowanie list ---");
            List<ScoreEntry> scores = new List<ScoreEntry>
            {
                new ScoreEntry("Ala", 120, 41.5f),
                new ScoreEntry("Olek", 300, 55.0f),
                new ScoreEntry("Ewa", 120, 38.2f),
                new ScoreEntry("Jan", 80, 30.0f),
            };

            scores.Sort((a, b) => b.Score.CompareTo(a.Score)); // malejąco
            Debug.Log($"Malejąco: {Show(scores)}");

            scores.Sort((a, b) =>
            {
                int byScore = b.Score.CompareTo(a.Score);
                return byScore != 0 ? byScore : a.Time.CompareTo(b.Time); // remis: szybszy wyżej
            });
            Debug.Log($"Remis po czasie: {Show(scores)}");

            List<ScoreEntry> top = scores.Take(10).ToList();
            Debug.Log($"Take(10): {top.Count} wpisy");

            var ordered = scores.OrderBy(s => s.Name.Length).ThenByDescending(s => s.Score);
            Debug.Log($"OrderBy(długość imienia).ThenByDescending(wynik): {Show(ordered)}");
        }

        private void LinqShort()
        {
            Debug.Log("--- Krótko o LINQ ---");
            List<EnemyInfo> enemies = MakeEnemies();
            enemies.Add(new Boss("Golem", 12f));

            var alive = enemies.Where(e => e.IsAlive);
            var names = enemies.Select(e => e.Name);
            var farthest = enemies.OrderByDescending(e => e.Distance).Take(5);
            EnemyInfo boss = enemies.FirstOrDefault(e => e is Boss);
            float total = enemies.Sum(e => e.Distance);
            bool anyNear = enemies.Any(e => e.Distance < 3f);

            Debug.Log($"Where(żywi): {Show(alive)}");
            Debug.Log($"Select(imiona): {Show(names)}");
            Debug.Log($"OrderByDescending(odległość).Take(5): {Show(farthest)}");
            Debug.Log($"FirstOrDefault(Boss): {boss}");
            Debug.Log($"Sum(odległości) = {total:F1}, Any(bliżej niż 3) = {anyNear}");
            // LINQ tworzy obiekty pośrednie (śmieci dla GC); w Update lepiej zwykła pętla.
        }

        private void Quiz()
        {
            Debug.Log("--- Sprawdź się: kolekcje ---");
            List<int> list = new List<int> { 5, 3, 8 };
            list.Remove(5);
            Debug.Log(list[0] + " " + list[1]); // oczekiwany wynik: 3 8

            Queue<int> q = new Queue<int>();
            q.Enqueue(2);
            q.Enqueue(4);
            Debug.Log(q.Dequeue() + " " + q.Peek()); // oczekiwany wynik: 2 4 (argumenty liczone od lewej)

            Stack<string> s = new Stack<string>();
            s.Push("y");
            s.Push("x");
            Debug.Log(s.Pop() + s.Pop()); // oczekiwany wynik: xy (stos oddaje ostatnio włożony)
        }

        private static List<EnemyInfo> MakeEnemies()
        {
            return new List<EnemyInfo>
            {
                new EnemyInfo("Zombi 1", "Zombi", 4.5f),
                new EnemyInfo("Szkielet 1", "Szkielet", 2.0f, isDead: true),
                new EnemyInfo("Zombi 2", "Zombi", 7.2f, isDead: true),
                new EnemyInfo("Pająk 1", "Pająk", 1.5f),
                new EnemyInfo("Zombi 3", "Zombi", 9.0f),
            };
        }
    }
}
