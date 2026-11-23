// Wyk4, slajd „UnityEvent”
using UnityEngine;
using UnityEngine.Events;

namespace Wyklady.Wyk4
{
    public class PressurePlate : MonoBehaviour
    {
        // Słuchaczy podpina się w Inspectorze, bez pisania kodu.
        [SerializeField] private UnityEvent pressed;
        [SerializeField] private UnityEvent<int> scored;
        [SerializeField] private int points = 50;

        // Dostęp dla słuchaczy dopisywanych z kodu (AddListener/RemoveListener).
        public UnityEvent Pressed => pressed;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            pressed.Invoke();
            scored.Invoke(points);
        }
    }
}
