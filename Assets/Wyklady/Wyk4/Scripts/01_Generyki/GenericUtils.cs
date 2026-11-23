// Wyk4, slajd „Metody generyczne”
using UnityEngine;

namespace Wyklady.Wyk4
{
    public static class GenericUtils
    {
        // Jedna metoda dla tablicy dowolnego typu: T ustala kompilator z argumentu.
        public static T RandomItem<T>(T[] items)
        {
            return items[Random.Range(0, items.Length)];
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
