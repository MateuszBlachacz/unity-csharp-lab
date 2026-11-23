// Wyk4, slajd „Subskrypcja: OnEnable i OnDisable”
using UnityEngine;
using UnityEngine.UI;

namespace Wyklady.Wyk4
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image fill;

        private void OnEnable()
        {
            health.Changed += UpdateBar;
            UpdateBar(health.Current, health.Max); // od razu bieżąca wartość, nie czekamy na pierwszą zmianę
        }

        private void OnDisable()
        {
            health.Changed -= UpdateBar;
        }

        private void UpdateBar(int current, int max)
        {
            fill.fillAmount = (float)current / max;
        }
    }
}
