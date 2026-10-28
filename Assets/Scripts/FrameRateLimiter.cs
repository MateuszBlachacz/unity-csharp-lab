using UnityEngine;

// Narzędzie testowe z L2: ogranicza liczbę klatek, żeby sprawdzić niezależność ruchu od FPS.
// Leży na obiekcie Demos, który poza testami jest wyłączony.
public class FrameRateLimiter : MonoBehaviour
{
    [SerializeField] private int targetFps = 30;

    private void Update()
    {
        // Przy włączonej synchronizacji pionowej targetFrameRate jest ignorowany.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
    }
}
