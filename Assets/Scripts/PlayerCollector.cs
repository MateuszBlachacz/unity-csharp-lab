using UnityEngine;

// Zbieranie monet z L3: gracz ma Rigidbody, monety mają collidery z Is Trigger.
public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private GameScore score;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Coin"))
        {
            return;
        }

        Coin coin = other.GetComponent<Coin>();
        if (coin == null)
        {
            return;
        }

        score.AddCoinPoints(coin.GetValue());
        score.CoinCollected(coin);
        Destroy(other.gameObject);
    }
}
