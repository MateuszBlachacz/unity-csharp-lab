// Wyk3, slajd „Klasa bazowa Enemy” (uproszczony menedżer gry: DemoGame.IsRunning)
using UnityEngine;

namespace Wyklady.Wyk3
{
    public static class DemoGame
    {
        public static bool IsRunning { get; private set; } = true;

        public static void TogglePause()
        {
            IsRunning = !IsRunning;
            Debug.Log(IsRunning ? "[Wyk3] Gra wznowiona" : "[Wyk3] Pauza");
        }

        // Pole statyczne przeżywa wyjście z trybu gry, gdy przeładowanie domeny jest wyłączone,
        // więc przy każdym starcie wracamy do stanu początkowego.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetState()
        {
            IsRunning = true;
        }
    }
}
