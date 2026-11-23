// Wyk4, slajdy „Słownik” i pokazowy „Rejestr przeciwników i ekwipunek”
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<string, int> items = new();

        public void Add(string itemId, int amount)
        {
            items.TryGetValue(itemId, out int n);
            items[itemId] = n + amount;
            Debug.Log($"Ekwipunek: {itemId} = {items[itemId]}");
        }

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb != null && kb.iKey.wasPressedThisFrame)
            {
                Print();
            }
        }

        private void Print()
        {
            if (items.Count == 0)
            {
                Debug.Log("Ekwipunek jest pusty");
                return;
            }

            foreach (KeyValuePair<string, int> pair in items)
            {
                Debug.Log($"Ekwipunek – {pair.Key}: {pair.Value}");
            }
        }
    }
}
