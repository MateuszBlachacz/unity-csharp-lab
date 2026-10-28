using UnityEngine;

// Moneta z L3: typ decyduje o kolorze i liczbie punktów.
public class Coin : MonoBehaviour
{
    [SerializeField] private CoinType type = CoinType.Bronze;

    public CoinType Type => type;

    private void Start()
    {
        // Odwołanie do material tworzy kopię materiału tylko dla tej monety.
        GetComponent<Renderer>().material.color = GetColor();
    }

    public void SetType(CoinType newType)
    {
        type = newType;
    }

    public int GetValue()
    {
        return type switch
        {
            CoinType.Bronze => 1,
            CoinType.Silver => 3,
            CoinType.Gold => 10,
            _ => 0
        };
    }

    private Color GetColor()
    {
        switch (type)
        {
            case CoinType.Bronze:
                return new Color(0.8f, 0.5f, 0.2f);
            case CoinType.Silver:
                return new Color(0.75f, 0.75f, 0.78f);
            case CoinType.Gold:
                return new Color(1f, 0.84f, 0f);
            default:
                return Color.white;
        }
    }
}
