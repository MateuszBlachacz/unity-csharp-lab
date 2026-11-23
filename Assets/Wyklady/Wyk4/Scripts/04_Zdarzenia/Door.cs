// Wyk4, slajd „UnityEvent” (słuchacz z Inspectora i słuchacz z kodu)
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private PressurePlate plate;
        [SerializeField] private float openHeight = 2.5f;
        [SerializeField] private float speed = 2f;

        private Vector3 closedPosition;
        private Vector3 target;

        private void Awake()
        {
            closedPosition = transform.position;
            target = closedPosition;
        }

        // Słuchacz dopisany z kodu; Open() jest podpięte osobno w Inspectorze płyty.
        private void OnEnable()
        {
            plate.Pressed.AddListener(OnPlatePressed);
        }

        private void OnDisable()
        {
            plate.Pressed.RemoveListener(OnPlatePressed);
        }

        private void OnPlatePressed()
        {
            Debug.Log("Płyta naciśnięta");
        }

        public void Open()
        {
            target = closedPosition + Vector3.up * openHeight;
            Debug.Log("Drzwi się otwierają");
        }

        private void Update()
        {
            // Płynny ruch bez korutyny: co klatkę krok w stronę celu.
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
