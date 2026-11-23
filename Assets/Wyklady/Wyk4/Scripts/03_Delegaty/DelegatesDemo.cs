// Wyk4, slajdy części 3: delegaty i wyrażenia lambda
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wyklady.Wyk4
{
    [DefaultExecutionOrder(-30)]
    public class DelegatesDemo : MonoBehaviour
    {
        private int score;

        private void Start()
        {
            Debug.Log("=== Wyk4, część 3: delegaty i wyrażenia lambda ===");
            DelegateType();
            ActionAndFunc();
            LambdasAndClosures();
            Multicast();
            Quiz();
        }

        // --- Delegat: typ dla metod ---
        private int Double(int d) => d * 2;
        private int Armor(int d) => Mathf.Max(d - 5, 0);
        private int Apply(int baseDamage, DamageModifier modifier) => modifier(baseDamage);

        private void DelegateType()
        {
            Debug.Log("--- Delegat: typ dla metod ---");
            DamageModifier mod = Double;
            Debug.Log(mod(10)); // 20
            mod = Armor;
            Debug.Log(mod(10)); // 5
            Debug.Log(Apply(12, Armor)); // 7 – metoda przekazana jako argument
        }

        // --- Gotowe delegaty: Action i Func ---
        // Uzupełnienia do slajdu: minimalne metody, żeby przykład się kompilował.
        private void ShowWinScreen() => Debug.Log("ShowWinScreen: wygrana");
        private void AddScore(int points)
        {
            score += points;
            Debug.Log($"AddScore({points}): wynik {score}");
        }
        private void UpdateBar(int current, int max) => Debug.Log($"UpdateBar: {current}/{max}");

        private void ActionAndFunc()
        {
            Debug.Log("--- Gotowe delegaty: Action i Func ---");
            Action onWin = ShowWinScreen;              // void, bez parametrów
            Action<int> onScore = AddScore;            // void, jeden parametr
            Action<int, int> onHealth = UpdateBar;     // void, dwa parametry
            Func<int> getScore = () => score;          // zwraca int
            Func<int, int, int> sum = (a, b) => a + b; // dwa int, zwraca int (ostatni typ to wynik)
            Func<Vector3, bool> inArena = p => Mathf.Abs(p.x) < 10f && Mathf.Abs(p.z) < 10f;
            Predicate<EnemyInfo> isDead = e => e.IsDead; // to samo co Func<EnemyInfo, bool>

            onWin();
            onScore(10);
            onHealth(70, 100);
            Debug.Log($"getScore() = {getScore()}, sum(2, 3) = {sum(2, 3)}, inArena((12, 0, 0)) = {inArena(new Vector3(12f, 0f, 0f))}");

            List<EnemyInfo> enemies = new List<EnemyInfo>
            {
                new EnemyInfo("Zombi", "Zombi", 3f),
                new EnemyInfo("Szkielet", "Szkielet", 5f, isDead: true),
            };
            int removed = enemies.RemoveAll(isDead);
            Debug.Log($"RemoveAll(isDead): usunięto {removed}, zostało {enemies.Count}");
        }

        // --- Wyrażenia lambda i domknięcia ---
        private void LambdasAndClosures()
        {
            Debug.Log("--- Wyrażenia lambda i domknięcia ---");
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (a, b) => a + b;
            Action<string> log = msg => Debug.Log(msg);
            Func<string, string> greet = name =>
            {
                string text = "Cześć, " + name;
                return text + "!";
            };

            log($"square(4) = {square(4)}, add(2, 3) = {add(2, 3)}");
            log(greet("Ala"));

            int bonus = 5;
            Func<int, int> withBonus = d => d + bonus;
            bonus = 1;
            // Oczekiwany wynik: 11. Lambda zapamiętuje zmienną bonus, a nie jej wartość
            // z chwili utworzenia, więc przy wywołaniu widzi już bonus = 1.
            Debug.Log(withBonus(10));
        }

        // --- Delegat z wieloma metodami ---
        private void PlaySound() => Debug.Log("PlaySound");
        private void DropLoot() => Debug.Log("DropLoot");

        private static int Count(Action a) => a == null ? 0 : a.GetInvocationList().Length;

        private void Multicast()
        {
            Debug.Log("--- Delegat z wieloma metodami ---");
            Action onDeath = null;
            onDeath += PlaySound;
            onDeath += DropLoot;
            Debug.Log($"Po dwóch +=: metod {Count(onDeath)}");
            onDeath?.Invoke(); // obie po kolei; ?. chroni przed null, gdy nikt nie słucha

            onDeath -= PlaySound;
            Debug.Log($"Po -= PlaySound: metod {Count(onDeath)}");

            Action logDeath = () => Debug.Log("Lambda: przeciwnik zginął");
            onDeath += logDeath;
            Debug.Log($"Po += lambda (zapamiętana w zmiennej): metod {Count(onDeath)}");
            onDeath?.Invoke();
            onDeath -= logDeath; // ta sama referencja, więc wypisanie działa
            Debug.Log($"Po -= tej samej referencji: metod {Count(onDeath)}");
        }

        // --- Sprawdź się: delegaty i lambdy ---
        private void Quiz()
        {
            Debug.Log("--- Sprawdź się: delegaty i lambdy ---");
            Func<int, int> f = x => x * 3;
            Debug.Log(f(3)); // oczekiwany wynik: 9

            Action a = () => Debug.Log("A");
            a += () => Debug.Log("B");
            a += () => Debug.Log("C");
            a(); // oczekiwany wynik: A, B, C (w kolejności dopisania)

            int total = 0;
            Action<int> addTo = n => total += n;
            addTo(5);
            addTo(10);
            Debug.Log(total); // oczekiwany wynik: 15 (lambda zmienia zmienną z zewnątrz)
        }
    }
}
