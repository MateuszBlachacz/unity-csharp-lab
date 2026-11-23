// Wyk4, slajd pokazowy „Rejestr przeciwników i ekwipunek” (przedmiot do zebrania)
using UnityEngine;

namespace Wyklady.Wyk4
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] private string itemId = "coin";
        [SerializeField] private int value = 10;
        [SerializeField] private float spinSpeed = 90f;

        private void Update()
        {
            transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !other.TryGetComponent(out Inventory inventory))
            {
                return;
            }

            inventory.Add(itemId, 1);
            if (itemId == "coin")
            {
                GameEvents.RaiseCoinCollected(value); // punkty liczy ScoreCounter, nie moneta
            }
            Destroy(gameObject);
        }
    }
}
