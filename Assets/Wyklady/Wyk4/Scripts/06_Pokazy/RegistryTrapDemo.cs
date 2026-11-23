// Wyk4, slajd pokazowy „Rejestr przeciwników i ekwipunek” (pułapka: zmiana listy w trakcie foreach)
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk4
{
    public class RegistryTrapDemo : MonoBehaviour
    {
        [SerializeField] private float radius = 4f;

        // Przeciwnicy wyłączeni w wersji z błędem; sprząta je wersja poprawna.
        private readonly List<GameObject> disabledByTrap = new();

        private void Update()
        {
            Keyboard kb = Keyboard.current;
            if (kb == null)
            {
                return;
            }

            if (kb.xKey.wasPressedThisFrame)
            {
                WrongWay();
            }
            else if (kb.cKey.wasPressedThisFrame)
            {
                RightWay();
            }
        }

        // SetActive(false) od razu woła OnDisable, a ono usuwa przeciwnika z Enemy.All w trakcie pętli.
        // Samo Destroy(e.gameObject) wyjątku by nie rzuciło: obiekt znika dopiero pod koniec klatki.
        private void WrongWay()
        {
            int count = 0;
            try
            {
                foreach (Enemy e in Enemy.All)
                {
                    if (e.Distance < radius)
                    {
                        e.gameObject.SetActive(false);
                        disabledByTrap.Add(e.gameObject);
                        count++;
                    }
                }
                Debug.Log($"X: wyłączono {count} przeciwników (nikt nie stał bliżej niż {radius})");
            }
            catch (InvalidOperationException ex)
            {
                Debug.Log($"Złapany wyjątek: {ex.GetType().Name}: {ex.Message} (wyłączono {count}, pętla przerwana)");
            }
        }

        // Pętla od końca: usunięcie elementu i nie przesuwa elementów jeszcze nieodwiedzonych.
        private void RightWay()
        {
            int count = 0;
            for (int i = Enemy.All.Count - 1; i >= 0; i--)
            {
                Enemy e = Enemy.All[i];
                if (e.Distance < radius)
                {
                    e.gameObject.SetActive(false);
                    Destroy(e.gameObject);
                    count++;
                }
            }

            foreach (GameObject go in disabledByTrap)
            {
                if (go != null)
                {
                    Destroy(go);
                }
            }
            disabledByTrap.Clear();
            Debug.Log($"C: usunięto {count} przeciwników bliżej niż {radius}, lista ma teraz {Enemy.All.Count}");
        }
    }
}
