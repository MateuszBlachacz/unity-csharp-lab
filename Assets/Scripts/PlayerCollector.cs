using UnityEngine;

// Zbieranie monet z L3: gracz ma kinematyczny Rigidbody, monety mają collidery z Is Trigger.
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

        score.AddPoints(coin.GetValue(), $"moneta {coin.Type}");
        score.CoinCollected();
        Destroy(other.gameObject);
    }
}
