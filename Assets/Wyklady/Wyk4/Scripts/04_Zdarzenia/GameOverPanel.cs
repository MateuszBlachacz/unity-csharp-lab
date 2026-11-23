// Wyk4, slajd pokazowy „Pasek zdrowia i Game Over” (przycisk Restart)
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wyklady.Wyk4
{
    public class GameOverPanel : MonoBehaviour
    {
        // Podpięte w Inspectorze: Button.onClick → GameOverPanel.Restart.
        public void Restart()
        {
#if UNITY_EDITOR
            // Scena pokazowa celowo nie jest w Build Settings, więc LoadScene(buildIndex) by nie zadziałało.
            // W edytorze przeładowujemy ją po ścieżce.
            UnityEditor.SceneManagement.EditorSceneManager.LoadSceneInPlayMode(
                SceneManager.GetActiveScene().path,
                new LoadSceneParameters(LoadSceneMode.Single));
#else
            // Wersja ze slajdu: w grze scena musi być dodana w Build Profiles.
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
#endif
        }
    }
}
