// Wyk4, slajd „Klasy generyczne”
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Bez „where T : Component” kompilator wie o T tylko tyle, że jest typem (object),
    // więc item.transform się nie skompiluje. Ograniczenie gwarantuje, że T ma transform.
    public class Registry<T> where T : Component
    {
        private readonly List<T> items = new();

        public int Count => items.Count;

        public void Register(T item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        public void Unregister(T item)
        {
            items.Remove(item);
        }

        public T Nearest(Vector3 point)
        {
            T best = null;
            float bestDist = float.MaxValue;
            foreach (T item in items)
            {
                float d = (item.transform.position - point).sqrMagnitude;
                if (d < bestDist)
                {
                    bestDist = d;
                    best = item;
                }
            }
            return best;
        }
    }
}
