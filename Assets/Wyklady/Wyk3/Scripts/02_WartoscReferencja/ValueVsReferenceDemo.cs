// Wyk3, slajdy „Struktura”, „Quiz: struct czy class?”, „Pułapka: transform.position”, „null”
using System.Collections;
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class ValueVsReferenceDemo : MonoBehaviour
    {
        // Rigidbody na obiekcie dziecku VelocityProbe (bez grawitacji i bez collidera).
        [SerializeField] private Rigidbody body;

        // Typy z quizu: identyczne pola, różnica tylko struct/class.
        private struct S { public int x; }
        private class C { public int x; }

        private static void SetS(S s) { s.x = 5; }
        private static void SetC(C c) { c.x = 5; }

        private void Start()
        {
            CopyStructAndReference();
            Quiz();
            PositionTrap();
            NullOperators();
            StartCoroutine(DestroyedObjectIsNull());
        }

        private void CopyStructAndReference()
        {
            var a = new DamageInfo(10, false, Vector3.zero);
            var b = a;          // kopia danych
            b.amount = 99;
            Debug.Log($"[Wyk3] struct: a = {a}, b = {b}");     // a nadal 10

            var w1 = new WeaponStats("Łuk", 8, 1f);
            var w2 = w1;        // kopia referencji, ten sam obiekt
            Debug.Log($"[Wyk3] class: w1 == w2: {w1 == w2}");

            DamageInfo empty = default;     // wszystkie pola wyzerowane
            Debug.Log($"[Wyk3] default(DamageInfo) = {empty}");
        }

        private void Quiz()
        {
            S s = new S { x = 1 };
            C c = new C { x = 1 };
            SetS(s);
            SetC(c);
            // Oczekiwany wynik: 1 5.
            // SetS dostaje kopię struktury i zmienia tylko tę kopię.
            // SetC dostaje kopię referencji wskazującą ten sam obiekt, więc zmiana jest widoczna.
            Debug.Log($"[Wyk3] Quiz struct czy class: {s.x} {c.x}");
        }

        private void PositionTrap()
        {
            // transform.position.x = 5f;
            // Błąd CS1612: position to właściwość zwracająca kopię Vector3 (struktury),
            // więc zmiana pola x tej kopii nic by nie dała i kompilator jej zabrania.

            // Sposób 1: kopia, zmiana, przypisanie z powrotem.
            Vector3 pos = transform.position;
            pos.x = 5f;
            transform.position = pos;

            // Sposób 2: nowy wektor.
            transform.position = new Vector3(5f, transform.position.y, transform.position.z);

            // Sposób 3: przesunięcie o wektor.
            transform.position += new Vector3(-5f, 0f, 0f);
            Debug.Log($"[Wyk3] Pozycja Demos po trzech sposobach: {transform.position}");

            // To samo dotyczy prędkości: body.linearVelocity.x = 2f; to też CS1612.
            Vector3 v = body.linearVelocity;
            v.x = 2f;
            body.linearVelocity = v;
            Debug.Log($"[Wyk3] linearVelocity po zmianie kopii: {body.linearVelocity}");
            body.linearVelocity = Vector3.zero;
        }

        private void NullOperators()
        {
            WeaponStats noWeapon = null;
            // ?. zwraca null zamiast rzucać NullReferenceException, ?? podaje wartość zastępczą.
            string text = noWeapon?.ToString() ?? "brak broni";
            Debug.Log($"[Wyk3] noWeapon?.ToString() ?? ... = {text}");

            int? bonus = null;
            Debug.Log($"[Wyk3] int? bonus: HasValue = {bonus.HasValue}, wartość lub 0 = {bonus ?? 0}");
            bonus = 5;
            Debug.Log($"[Wyk3] int? bonus = 5: HasValue = {bonus.HasValue}, Value = {bonus.Value}");
        }

        private IEnumerator DestroyedObjectIsNull()
        {
            var temp = new GameObject("W3_Temp");
            Destroy(temp);
            // Destroy działa pod koniec klatki, więc w tej klatce obiekt jeszcze istnieje.
            Debug.Log($"[Wyk3] Zaraz po Destroy: temp == null: {temp == null}");

            yield return null;

            // Unity przeciąża == dla UnityEngine.Object: zniszczony obiekt jest równy null,
            // choć zmienna C# nadal trzyma referencję (rzutowanie na object omija przeciążenie).
            Debug.Log($"[Wyk3] Klatkę później: temp == null: {temp == null}, (object)temp == null: {(object)temp == null}");
            // Dlatego na obiektach Unity nie używamy ?. ani ??: one sprawdzają tylko prawdziwy null C#.
            // string n = temp?.name; // tu rzuciłoby MissingReferenceException, bo temp nie jest null w sensie C#
        }
    }
}
