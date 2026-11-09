// Wyk3, slajdy „Własna klasa”, „Konstruktory”, „Hermetyzacja w praktyce”, „Wszystko dziedziczy po object”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class ClassesDemo : MonoBehaviour
    {
        private void Start()
        {
            // Zwykłe klasy C# tworzymy przez new.
            var pistol = new WeaponStats("Pistolet", 10, 2f);
            Debug.Log($"[Wyk3] {pistol}, odstęp {pistol.Cooldown} s, krytyk {pistol.DamageFor(true)}");

            var coin = new PickupData("coin");
            var gem = new PickupData("gem", 5) { Lifetime = 5f };
            Debug.Log($"[Wyk3] PickupData: {coin} | {gem}");

            // Komponentu nie tworzymy przez new:
            // var hp = new HealthEncapsulated();
            // MonoBehaviour musi być przypięty do GameObject. Obiekt z new nie należy do żadnego
            // obiektu sceny, Unity nie wywołuje jego metod (Awake, Update) i zgłasza ostrzeżenie.
            HealthEncapsulated health = gameObject.AddComponent<HealthEncapsulated>();
            health.TakeDamage(30);
            health.Heal(10);
            Debug.Log($"[Wyk3] HealthEncapsulated po -30 i +10: {health.Current}/{health.Max}");

            // Wszystko dziedziczy po object: ToString, GetType, porównanie ==.
            var p = new WeaponStats("Miecz", 15, 1f);
            var q = new WeaponStats("Miecz", 15, 1f);
            WeaponStats r = p;
            Debug.Log($"[Wyk3] p.ToString() = {p}, p.GetType().Name = {p.GetType().Name}");
            // Klasa nie przeciąża ==, więc porównywane są referencje:
            // p == q daje False (dwa różne obiekty o tych samych danych), p == r daje True (ten sam obiekt).
            Debug.Log($"[Wyk3] p == q: {p == q}, p == r: {p == r}");
        }
    }
}
