// Wyk4, slajd „Ograniczenia parametru typu” (po jednej metodzie na wiersz tabeli)
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    public static class ConstraintExamples
    {
        // where T : class – typ referencyjny, więc wolno zwrócić null.
        public static T FirstOrNull<T>(List<T> list) where T : class
        {
            return list.Count > 0 ? list[0] : null;
        }

        // where T : struct – typ wartościowy, nigdy null; default(T) to „same zera”.
        public static bool IsDefault<T>(T value) where T : struct
        {
            return value.Equals(default(T));
        }

        // where T : new() – typ ma konstruktor bezparametrowy, więc wolno napisać new T().
        public static T Create<T>() where T : new()
        {
            return new T();
        }

        // where T : Component – można użyć API komponentów (GetComponent, AddComponent).
        public static T GetOrAdd<T>(GameObject go) where T : Component
        {
            return go.TryGetComponent(out T existing) ? existing : go.AddComponent<T>();
        }

        // where T : IDamageable – można wywołać metody interfejsu.
        public static int DamageAll<T>(IEnumerable<T> targets, int amount) where T : IDamageable
        {
            int hit = 0;
            foreach (T t in targets)
            {
                if (t.IsAlive)
                {
                    t.TakeDamage(amount);
                    hit++;
                }
            }
            return hit;
        }

        // where T : IComparable<T> – elementy można porównywać metodą CompareTo.
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        // Ograniczenie łączone: komponent (ma name, gameObject) i zarazem cel obrażeń.
        public static string Describe<T>(T target) where T : MonoBehaviour, IDamageable
        {
            return $"{target.name}: żyje = {target.IsAlive}";
        }
    }
}
