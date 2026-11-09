// Wyk3, slajd „Zadanie projektowe: klasy pochodne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Coin : Pickup
    {
        [SerializeField] private int value = 1;

        protected override void Apply(DemoPlayer p)
        {
            p.AddScore(value);
        }
    }
}
