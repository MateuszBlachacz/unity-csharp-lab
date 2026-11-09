// Wyk3, slajdy „Konstruktory w hierarchii: base”, „protected, sealed i ukrywanie metod”,
// „Sprawdzanie typu: is, as, switch”, „Quiz: kolejność konstruktorów”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class InheritanceDemo : MonoBehaviour
    {
        // Klasy z quizu o kolejności konstruktorów.
        private class A
        {
            public A()
            {
                Debug.Log("[Wyk3] A()");
                Init();
            }

            protected virtual void Init()
            {
                Debug.Log("[Wyk3] A.Init");
            }
        }

        private class B : A
        {
            private int value = 100;

            public B()
            {
                Debug.Log($"[Wyk3] B() {value}");
            }

            protected override void Init()
            {
                Debug.Log($"[Wyk3] B.Init {value}");
            }
        }

        private void Start()
        {
            // Konstruktory w hierarchii: najpierw Item(...), potem ItemWeapon(...).
            var sword = new ItemWeapon("Miecz", 12);
            Item item = sword;      // referencja typu bazowego do obiektu pochodnego
            Debug.Log($"[Wyk3] item.Describe(): {item.Describe()}");

            // override kontra new.
            DescribedEnemy enemy = new Boss();
            Debug.Log($"[Wyk3] Przez DescribedEnemy: Describe() = {enemy.Describe()}, Kind() = {enemy.Kind()}");
            Debug.Log($"[Wyk3] Przez Boss: Kind() = {((Boss)enemy).Kind()}");
            // Describe jest wirtualna, więc działa wersja z Boss; Kind jest ukryta przez new,
            // więc przez zmienną typu DescribedEnemy działa wersja bazowa.

            // is, as, switch po typie.
            var key = new Item("Klucz");
            if (item is ItemWeapon weapon)
            {
                Debug.Log($"[Wyk3] item is ItemWeapon: obrażenia {weapon.Damage}");
            }

            ItemWeapon notWeapon = key as ItemWeapon;   // as przy złym typie daje null
            Debug.Log($"[Wyk3] key as ItemWeapon == null: {notWeapon == null}");
            // ItemWeapon bad = (ItemWeapon)key;
            // Rzutowanie przy złym typie kompiluje się, ale w czasie działania rzuca InvalidCastException.

            Debug.Log($"[Wyk3] Label: {Label(sword)} | {Label(key)} | {Label(null)}");

            // Quiz: oczekiwana kolejność A(), B.Init 100, B() 100.
            // Inicjalizatory pól klasy pochodnej (value = 100) działają przed konstruktorem bazowym,
            // a metoda wirtualna wywołana w konstruktorze A trafia do wersji z B.
            new B();
        }

        private static string Label(Item item)
        {
            return item switch
            {
                ItemWeapon w => $"broń {w.Name} ({w.Damage})",
                null => "nic",
                _ => $"przedmiot {item.Name}"
            };
        }
    }
}
