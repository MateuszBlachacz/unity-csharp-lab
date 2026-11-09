// Wyk3: obsługa pauzy w scenie pokazowej (klasa statyczna DemoGame nie ma Update)
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wyklady.Wyk3
{
    public class DemoGameInput : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
            {
                DemoGame.TogglePause();
            }
        }
    }
}
