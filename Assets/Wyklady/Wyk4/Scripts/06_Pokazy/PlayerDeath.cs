// Wyk4, omówienie zadania projektowego „Zdarzenia w Arenie” (sytuacja 3: śmierć gracza)
using UnityEngine;

namespace Wyklady.Wyk4
{
    // Lokalne zdarzenie Health.Died gracza zamieniane na globalne GameEvents.GameOver:
    // spawner, osiągnięcia i inne systemy nie muszą znać obiektu gracza.
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private DemoPlayer controls;

        private void OnEnable()
        {
            health.Died += OnDied;
        }

        private void OnDisable()
        {
            health.Died -= OnDied;
        }

        private void OnDied()
        {
            controls.enabled = false;
            Debug.Log("Gracz pokonany: koniec gry");
            GameEvents.RaiseGameOver();
        }
    }
}
