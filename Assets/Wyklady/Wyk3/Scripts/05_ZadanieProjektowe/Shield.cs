// Wyk3, slajd „Zadanie projektowe: klasy pochodne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Shield : Pickup
    {
        [SerializeField] private float duration = 5f;

        protected override void Apply(DemoPlayer p)
        {
            p.SetInvulnerable(duration);
        }
    }
}
