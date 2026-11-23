// Wyk4, slajd „Subskrypcja: OnEnable i OnDisable”
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private GameObject panel;

        private void OnEnable()
        {
            playerHealth.Died += Show;
        }

        private void OnDisable()
        {
            playerHealth.Died -= Show; // bez wypisania zniszczony obiekt zostałby na liście słuchaczy
        }

        private void Show()
        {
            panel.SetActive(true);
        }
    }
}
