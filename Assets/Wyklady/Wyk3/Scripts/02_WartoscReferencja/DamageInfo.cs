// Wyk3, slajd „Struktura”
using UnityEngine;

namespace Wyklady.Wyk3
{
    // Struktura to typ wartościowy: przypisanie i przekazanie do metody kopiują całe dane.
    public struct DamageInfo
    {
        public int amount;
        public bool isCritical;
        public Vector3 hitPoint;

        public DamageInfo(int amount, bool isCritical, Vector3 hitPoint)
        {
            this.amount = amount;
            this.isCritical = isCritical;
            this.hitPoint = hitPoint;
        }

        public override string ToString()
        {
            return $"{amount}{(isCritical ? " (krytyk)" : "")} w {hitPoint}";
        }
    }
}
