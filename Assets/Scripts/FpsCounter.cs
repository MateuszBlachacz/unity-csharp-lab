using System.Globalization;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private static readonly CultureInfo Polish = new CultureInfo("pl-PL");

    private float elapsed;
    private int frames;
    private float longestFrame;

    private void Update()
    {
        // unscaledDeltaTime nie zależy od Time.timeScale, więc pauza nie psuje pomiaru.
        float delta = Time.unscaledDeltaTime;
        elapsed += delta;
        frames++;
        longestFrame = Mathf.Max(longestFrame, delta);

        if (elapsed < 1f)
        {
            return;
        }

        float fps = frames / elapsed;
        Debug.Log(string.Format(Polish, "FPS: {0:F1}, najdłuższa klatka: {1:F1} ms", fps, longestFrame * 1000f));

        elapsed = 0f;
        frames = 0;
        longestFrame = 0f;
    }
}
