// Wyk3, slajd „Zadanie projektowe: klasy pochodne”
using UnityEngine;

namespace Wyklady.Wyk3
{
    public class Medkit : Pickup
    {
        [SerializeField] private int amount = 25;

        // Przy pełnym zdrowiu apteczka zostaje na scenie.
        protected override bool CanPickUp(DemoPlayer p)
        {
            return p.Health.Current < p.Health.Max;
        }

        protected override void Apply(DemoPlayer p)
        {
            p.Health.Heal(amount);
        }
    }
}
