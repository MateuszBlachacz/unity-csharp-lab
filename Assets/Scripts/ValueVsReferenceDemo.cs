using UnityEngine;

// Zadanie 5 (L6): ta sama zmiana parametru w metodzie dla struktury i dla klasy.
//
// Przewidywanie (przed uruchomieniem):
//   struktura bez ref: amount zostanie 10; klasa: amount zmieni się na 999;
//   struktura z ref: amount zmieni się na 999.
//
// Wyjaśnienie wyniku:
//   DamageInfoStruct to typ wartościowy. Przy wywołaniu SetAmount(s) metoda dostaje kopię
//   całej struktury i zmienia tylko tę kopię, więc zmienna w Start dalej ma 10.
//   DamageInfoClass to typ referencyjny. Zmienna przechowuje referencję do obiektu;
//   metoda dostaje kopię referencji, ale ta kopia wskazuje ten sam obiekt, więc zmiana jest
//   widoczna po powrocie (999).
//   Słowo ref przekazuje samą zmienną zamiast kopii, więc metoda zmienia oryginalną strukturę (999).
//   Tak samo działają typy Unity: Vector3 jest strukturą (dlatego transform.position.x = 1
//   się nie kompiluje), a GameObject i komponenty są klasami.
public class ValueVsReferenceDemo : MonoBehaviour
{
    private struct DamageInfoStruct
    {
        public int amount;
        public Vector3 direction;
    }

    private class DamageInfoClass
    {
        public int amount;
        public Vector3 direction;
    }

    private void Start()
    {
        var s = new DamageInfoStruct { amount = 10, direction = Vector3.forward };
        var c = new DamageInfoClass { amount = 10, direction = Vector3.forward };

        SetAmount(s);
        Debug.Log($"Struktura bez ref: amount = {s.amount}");   // 10

        SetAmount(c);
        Debug.Log($"Klasa: amount = {c.amount}");               // 999

        SetAmount(ref s);
        Debug.Log($"Struktura z ref: amount = {s.amount}");     // 999
    }

    private static void SetAmount(DamageInfoStruct info)
    {
        info.amount = 999;
    }

    private static void SetAmount(DamageInfoClass info)
    {
        info.amount = 999;
    }

    private static void SetAmount(ref DamageInfoStruct info)
    {
        info.amount = 999;
    }
}
