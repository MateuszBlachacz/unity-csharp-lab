using UnityEngine;

// Zadanie 4 (L4): w każdej klatce szuka monety najbliższej graczowi i kieruje na nią strzałkę.
public class CoinFinder : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    // Odległość strzałki od środka gracza w poziomie.
    [SerializeField] private float arrowDistance = 0.9f;

    private void Update()
    {
        // Pusta tablica, gdy nie ma już monet; pętla wtedy się nie wykona.
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        GameObject closest = null;
        float best = float.MaxValue;
        foreach (GameObject coin in coins)
        {
            // Kwadrat odległości wystarcza do porównań i nie wymaga pierwiastka.
            float distSqr = (coin.transform.position - transform.position).sqrMagnitude;
            if (distSqr < best)
            {
                best = distSqr;
                closest = coin;
            }
        }

        arrow.gameObject.SetActive(closest != null);
        if (closest == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, closest.transform.position, Color.yellow);

        Vector3 dir = closest.transform.position - transform.position;
        dir.y = 0f;                                   // tylko w poziomie
        if (dir != Vector3.zero)
        {
            arrow.rotation = Quaternion.LookRotation(dir);
            // Strzałka krąży wokół gracza na stałej wysokości, żeby zawsze była wysunięta
            // w stronę monety. Gracz się nie obraca, więc kierunek świata działa też lokalnie.
            Vector3 offset = dir.normalized * arrowDistance;
            arrow.localPosition = new Vector3(offset.x, arrow.localPosition.y, offset.z);
        }
    }
}
