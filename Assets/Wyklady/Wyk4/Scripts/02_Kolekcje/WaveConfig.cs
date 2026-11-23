// Wyk4, slajd „Kolekcje w Inspectorze i inicjalizacja”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Zaraz po CollectionsDemo, żeby log słownika był w części 2.
    [DefaultExecutionOrder(-39)]
    public class WaveConfig : MonoBehaviour
    {
        // List<T> i tablice Unity pokazuje w Inspectorze; Dictionary – nie.
        [SerializeField] private List<GameObject> prefabs = new();
        [SerializeField] private List<int> sizes = new() { 3, 5, 8 };

        [System.Serializable]
        public struct Entry
        {
            public string id;
            public int amount;
        }

        // Słownik wypełniany w Inspectorze przez listę par.
        [SerializeField] private List<Entry> start = new();

        private Dictionary<string, int> inventory;

        private void Awake()
        {
            inventory = new Dictionary<string, int>();
            foreach (Entry e in start)
            {
                inventory[e.id] = e.amount;
            }
        }

        private void Start()
        {
            Debug.Log("--- Kolekcje w Inspectorze i inicjalizacja ---");
            Debug.Log($"WaveConfig: {prefabs.Count} prefaby, fale [{string.Join(", ", sizes)}]");
            foreach (KeyValuePair<string, int> pair in inventory)
            {
                Debug.Log($"Słownik z listy start: {pair.Key} = {pair.Value}");
            }
        }
    }
}
